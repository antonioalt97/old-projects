import os
import json
from django.shortcuts import render
from django.http import JsonResponse
from datetime import datetime
from home.models import Reserva, Menu, MensajeContacto, Usuario, Rol, Mesa
from django.contrib.auth.models import User
from datetime import datetime, date
from django.shortcuts import get_object_or_404
from sendgrid import SendGridAPIClient
from sendgrid.helpers.mail import Mail, To, Content, From
from dotenv import load_dotenv
from datetime import timedelta
from admin_volt.forms import ContactMessageForm
from django.views.decorators.http import require_http_methods
from django.utils import timezone

load_dotenv()

API_KEY = os.environ.get('SENDGRID_API_KEY')

def index(request):
        return render(request, 'pages/index.html')

''' Funciones de reservas '''
def verificarReservaExistente(mesa, fecha):
    reservas = Reserva.objects.filter(mesa=mesa, fecha_reserva=fecha)
    return reservas.exists()

def obtener_mesa_disponible(fecha):
    todas_las_mesas = Mesa.objects.all()

    mesas_disponibles = []
    for mesa in todas_las_mesas:
        if not verificarReservaExistente(mesa, fecha):
            mesas_disponibles.append(mesa)

    return mesas_disponibles

def verificar_antiguedad_reserva(fecha_reserva):
    fecha_actual = datetime.now()
    fecha_reserva = datetime.combine(fecha_reserva, datetime.min.time())
    diferencia = fecha_reserva - fecha_actual
    horas_minimas_anticipacion = timedelta(hours=24)
    es_mayor = diferencia > horas_minimas_anticipacion
    return es_mayor
    

def confirmar_reserva(user, mesa, first_name, last_name, fecha, tiempo, cantidad_comensales, comentario):
    if not verificarReservaExistente(mesa, fecha):
        nueva_reserva = Reserva.objects.create(
            user=user,
            nombre=first_name,
            apellido=last_name,
            fecha_reserva=fecha,
            tiempo=tiempo,
            cantidad_comensales=cantidad_comensales,
            estado_reserva='Activa',
            comentario=comentario,
            mesa=mesa
        )

        if nueva_reserva:
            return True

    return False

def create_reserve(request):
    if request.method == 'POST':
        user = request.user
        reserve = request.POST

        if user.groups.exists():
            group_name = user.groups.first().name
            print(group_name)
        else:
            group_name = None

        if group_name not in ["Administración", "Clientes"]:
            return JsonResponse({'error': 'Invalid credentials'}, status=400)
        
        first_name = reserve.get("first_name")
        last_name = reserve.get("last_name")
        email = reserve.get("email")

        fecha = reserve.get("fecha")
        tiempo = reserve.get("time")
        cantidad_comensales = reserve.get("number-guests")
        comentario = reserve.get("comentario")

        estado_reserva = 'Activa'

        date = datetime.strptime(fecha, '%m/%d/%Y').date()
        hora = datetime.strptime(tiempo, '%H:%M').time()

        mesas_disponibles = obtener_mesa_disponible(date)

        reserva_exitosa = confirmar_reserva(user, mesas_disponibles[0], first_name, last_name, date, hora, cantidad_comensales, comentario)

        if reserva_exitosa:
            name = first_name + ' ' + last_name
            responser = send_email(name, email, mesas_disponibles[0], fecha, tiempo, cantidad_comensales, comentario)
            if responser:
                response = {'message': 'Reserva creada correctamente'}
                return JsonResponse(response, status=200)
            else:
                response = {'message': 'Reserva creada correctamente, pero no se pudo enviar el correo'}
                return JsonResponse(response, status=200)
        else:
            response = {'message': 'Error al crear la reserva'}
            return JsonResponse(response, status=400)
    else:
        return JsonResponse({'error': 'Invalid method'}, status=400)
    



@require_http_methods(["PUT"])
def modificar_reserva(request, pk):
    try:
        reserva = get_object_or_404(Reserva, pk=pk)

        data = json.loads(request.body)
        print(data)

        date = datetime.strptime(data["fecha"],'%m/%d/%Y').date()
        hora = datetime.strptime(data["time"],'%H:%M').time()
        if not verificar_antiguedad_reserva(date):
            print("No se puede modificar una reserva con menos de 12 horas de anticipación")
            return JsonResponse({'message': 'No se puede modificar una reserva con menos de 12 horas de anticipación'}, status=400)

        reserva.name = data["name"]
        reserva.apellido = data["last_name"]
        reserva.fecha_reserva = date
        reserva.tiempo = hora
        reserva.cantidad_comensales = data["number-guests"]
        reserva.comentario = data["message"]

        reserva.save()
        return JsonResponse({'message': 'Reserva actualizada correctamente'})
    except Reserva.DoesNotExist:
        return JsonResponse({'message': 'La reserva no existe'}, status=404)
    
def delete_reserve(request):
    if request.method == 'POST':
        print(request.body)
        data = json.loads(request.body)
        print(data)
        for reserva_id in data:
            reserva = get_object_or_404(Reserva, id=reserva_id)
            reserva.delete()
        return JsonResponse({'message': 'Reservas eliminadas correctamente'})
    return JsonResponse({'message': 'Solicitud no válida'}, status=400)

''' Funciones de menú '''
def get_menus(request):
    today = date.today()
    menu_day = Menu.objects.filter(fecha=today)
    menu_details = []

    if menu_day.exists():
        for menu in menu_day:
            imagen_url = menu.imagen.url if menu.imagen else None
            menu_info = {
                'plato': menu.plato,
                'fecha': menu.fecha,
                'imagen': imagen_url,
                'descripcion': menu.descripcion,
            }
            print(menu_info)
            menu_details.append(menu_info)

        return JsonResponse({'menu_del_dia': menu_details})
    else:
        return JsonResponse({'message': 'Hoy no hay un menú disponible'}, status=404)

''' Funciones de correo '''
def send_email(name, email, nro_mesa, fecha, hora, cantidad_comensales, comentario):
    message = Mail(
        from_email='siglomaster21@gmail.com',
        to_emails='siglomaster21@gmail.com',
        subject='Reserva realizada con éxito',
        html_content=
        '''
        <html>
        <head>
        <style>
        table {
        font-family: arial, sans-serif;
        border-collapse: collapse;
        width: 100%;
        }
        td, th {
        border: 1px solid #dddddd;
        text-align: left;
        padding: 8px;
        }
        tr:nth-child(even) {
        background-color: #dddddd;
        }
        </style>
        </head>
        <body>
        <h2>Reserva</h2>
        <table>
        <tr>
        <th>Nombre</th>
        <th>Nro. Mesa</th>
        <th>Fecha</th>
        <th>Hora</th>
        <th>Cantidad de comensales</th>
        <th>Comentario</th>
        </tr>
        <tr>
        <td>'''+ name +'''</td>
        <td>'''+ str(nro_mesa) +'''</td>
        <td>'''+ str(fecha) +'''</td>
        <td>'''+ str(hora) +'''</td>
        <td>'''+ str(cantidad_comensales) +'''</td>
        <td>'''+ str(comentario) +'''</td>
        </tr>
        </table>
        </body>
        </html>
        ''')
    try:
        sg = SendGridAPIClient(API_KEY)
        response = sg.send(message)
        print(response.status_code)
        print(response.body)
        print(response.headers)
        return True
    except Exception as e:
        print(e.message)
        return False

def send_contact_email(nombre, email, subject, message):
    message = Mail(
        from_email='siglomaster21@gmail.com',
        to_emails=email,
        subject="Gracias por contactarnos!",
        html_content=
        '''
        <html>
        <head>

        </head>
        <body>
        <h2>Hola ''' + nombre + '''!</h2>
        <p>Este es tu mensaje: ''' + message + ''', asunto: ''' + subject + '''</p>
        <p>Gracias por contactarnos, te contactaremos lo antes posible.</p>
        </body>
        </html>
        ''')
    try:
        sg = SendGridAPIClient(API_KEY)
        response = sg.send(message)
        print(response.status_code)
        print(response.body)
        print(response.headers)
        return True
    except Exception as e:
        print(e.message)
        return False

''' Funciones de contacto '''
def contact_message(request):
    if request.method == 'POST':
        form = ContactMessageForm(request.POST)
        if form.is_valid():
            name = form.cleaned_data['name']
            email = form.cleaned_data['email']
            subject = form.cleaned_data['subject']
            message = form.cleaned_data['message']

            new_message = MensajeContacto.objects.create(
                nombre=name,
                correo_electronico=email,
                asunto=subject,
                mensaje=message
                )

            if new_message:
                responser = send_contact_email(name, email, subject, message)
                if responser:
                    response = {'message': 'Mensaje enviado correctamente, te hemos enviado un correo de confirmación'}
                else:
                    response = {'message': 'Mensaje enviado correctamente, pero no se pudo enviar el correo de confirmación'}
                return JsonResponse(response, status=200)
            else:
                response = {'message': 'Error al enviar el mensaje'}
                return JsonResponse(response, status=400)
                
        else:
            response = {'message': 'Error al enviar el mensaje'}
            return JsonResponse(response, status=400)
    else:
        return JsonResponse({'error': 'Invalid method'}, status=400)

''' Funciones de usuarios '''
def modify_user(request):
    if request.method == "POST":
        data = request.POST
        user = request.user
        group = user.groups.first().name
        print(group)

        if group:
            try:
                rol_instance = Rol.objects.get(id_rol=1)
            except Rol.MultipleObjectsReturned:
                return JsonResponse({'message': 'Multiple Rol instances found for this group'}, status=400)
        else:
            return JsonResponse({'message': 'Group does not exist'}, status=400)

        usuario, created = Usuario.objects.get_or_create(id_usuario=user.id)
        usuario.nombre = data.get("first_name")
        usuario.apellido = data.get("last_name")
        usuario.correo_electronico = data.get("email")
        usuario.telefono = data.get("phone")
        usuario.direccion = data.get("address") + " " + str(data.get("number")) + " " + data.get("region") + " " + data.get("comuna")
        usuario.rol_id_rol = rol_instance

        is_active = "Activo" if user.is_active else "Inactivo"
        usuario.estado_usuario = is_active
        usuario.save()
        return JsonResponse({'message': 'Usuario actualizado correctamente'})
    else:
        return JsonResponse({'error': 'Invalid method'}, status=400)