from django.shortcuts import render, redirect
from admin_volt.forms import RegistrationForm, LoginForm, UserPasswordResetForm, UserPasswordChangeForm, UserSetPasswordForm
from django.contrib.auth.views import LoginView, PasswordResetView, PasswordChangeView, PasswordResetConfirmView
from django.contrib.auth import logout
from django.contrib.auth.decorators import login_required
from django.contrib.auth.decorators import login_required, user_passes_test
from django.shortcuts import redirect
from home.models import Reserva, Menu
from django.contrib.auth.models import Group
from datetime import datetime
import pytz
from django.utils.timezone import make_aware

santiago_tz = pytz.timezone('America/Santiago')

def group_required(*group_names):
    def in_groups(u):
        if u.is_authenticated:
            if bool(u.groups.filter(name__in=group_names)) or u.is_superuser:
                return True
        return False
    return user_passes_test(in_groups)

def index(request):
  return render(request, 'pages/index.html')

from datetime import timedelta


@login_required(login_url="/accounts/login/")
def dashboard(request):
    reservas = []
    menu_details = []
    hora_santiago = datetime.now(santiago_tz)

    menu_day = Menu.objects.filter(fecha=hora_santiago.date())
    groups = Group.objects.filter(user=request.user)
    group_names = [group.name for group in groups]

    if "Clientes" in group_names:
        reservas = Reserva.objects.filter(user=request.user)
        reservas = reservas[::-1][:5]


    if "Administración" in group_names:
        reservas = Reserva.objects.all()
        reservas = reservas[::-1][:15]
    cantidad_reservas = len(reservas)
    if menu_day.exists():
        for menu in menu_day:
            menu_info = {
                'plato': menu.plato,
                'fecha': menu.fecha,
                'imagen': menu.imagen,
                'descripcion': menu.descripcion,
                'tipo_menu': menu.tipo_menu.tipo_menu,
            }
            menu_details.append(menu_info)

    context = {
        'segment': 'dashboard',
        'reservas': reservas,
        'menu_details': menu_details,
        'disable': False,
        'cantidad_reservas': cantidad_reservas,
    }
    return render(request, 'pages/dashboard.html', context)

@login_required
@group_required('Clientes', 'Administración')
def settings(request):
  context = {
    'segment': 'settings'
  }
  return render(request, 'pages/settings.html', context)

@login_required
@group_required('Clientes', 'Administración')
def bs_tables(request):
  reservas = []
  groups = Group.objects.filter(user=request.user)
  current_time = datetime.now(santiago_tz)
  group_names = [group.name for group in groups]
  if "Clientes" in group_names:
    reservas = Reserva.objects.filter(user=request.user)
    reservas = reservas[::-1][:5]
      
    for reserva in reservas:
      reserva_datetime = make_aware(datetime.combine(reserva.fecha_reserva, reserva.tiempo), santiago_tz)
      time_difference = reserva_datetime - current_time
      print(time_difference)
      if time_difference < timedelta(days=1):
          reserva.disable = True
      else:
          reserva.disable = False
      print(reserva.disable)
  if "Administración" in group_names:
    reservas = Reserva.objects.all()
    reservas = reservas[::-1][:15]
  context = {
    'parent': 'tables',
    'segment': 'bs_tables',
    'reservas': reservas,
  }
  return render(request, 'pages/tables/bootstrap-tables.html', context)

def register_view(request):
  if request.method == 'POST':
    form = RegistrationForm(request.POST)
    if form.is_valid():
      print("Account created successfully!")
      form.save()
      return redirect('/accounts/login/')
    else:
      print("Registration failed!")
  else:
    form = RegistrationForm()
  
  context = { 'form': form }
  return render(request, 'accounts/sign-up.html', context)

class UserLoginView(LoginView):
  form_class = LoginForm
  template_name = 'accounts/sign-in.html'

class UserPasswordChangeView(PasswordChangeView):
  template_name = 'accounts/password-change.html'
  form_class = UserPasswordChangeForm

class UserPasswordResetView(PasswordResetView):
  template_name = 'accounts/forgot-password.html'
  form_class = UserPasswordResetForm

class UserPasswrodResetConfirmView(PasswordResetConfirmView):
  template_name = 'accounts/reset-password.html'
  form_class = UserSetPasswordForm

def logout_view(request):
  logout(request)
  return redirect('/')

def lock(request):
  return render(request, 'accounts/lock.html')

def error_404(request):
  return render(request, 'pages/examples/404.html')

def error_500(request):
  return render(request, 'pages/examples/500.html')

