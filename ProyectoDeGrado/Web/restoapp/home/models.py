from django.db import models
from django.contrib.auth.models import User
from datetime import datetime
from django.utils import timezone

class Cliente(models.Model):
    user = models.OneToOneField(User, on_delete=models.CASCADE, default=1)
    adress = models.CharField(max_length=50)
    region = models.CharField(max_length=50, default="Región Metropolitana")
    comuna = models.CharField(max_length=50, default="Santiago")
    phone = models.CharField(max_length=50)
    class Meta:
        db_table = 'CLIENTE'

class Rol(models.Model):
    id_rol = models.BigIntegerField(primary_key=True)
    descripcion = models.CharField(max_length=25)

    class Meta:
        db_table = 'rol'

class Usuario(models.Model):
    id_usuario = models.BigIntegerField(primary_key=True)
    contrasena = models.CharField(max_length=150)
    nombre = models.CharField(max_length=30)
    apellido = models.CharField(max_length=30)
    email = models.CharField(max_length=100)
    direccion = models.CharField(max_length=100, blank=True, null=True)
    estado_usuario = models.CharField(max_length=1)
    telefono = models.CharField(max_length=25, blank=True, null=True)
    genero = models.CharField(max_length=25, blank=True, null=True)
    rol_id_rol = models.ForeignKey(Rol, default=1, on_delete=models.CASCADE)

    class Meta:
        db_table = 'usuario'
        unique_together = (('id_usuario', 'rol_id_rol'),)

class Mesa(models.Model):
    id_mesa = models.BigIntegerField(primary_key=True)
    numero_mesa = models.BigIntegerField()
    capacidad = models.BigIntegerField(blank=True, null=True)
    descripcion = models.CharField(max_length=200)
    en_uso = models.CharField(max_length=1)
    activa = models.CharField(max_length=1)

    class Meta:
        db_table = 'mesa'

class Tipo_menu(models.Model):
    tipo_menu = models.CharField(max_length=25)
    class Meta:
        db_table = 'TIPO_MENU'

class Empleado(models.Model):
    user = models.OneToOneField(User, on_delete=models.CASCADE, default=1) 
    address = models.CharField(max_length=50)
    region = models.CharField(max_length=50, default="Región Metropolitana")
    comuna = models.CharField(max_length=50, default="Santiago")
    phone = models.CharField(max_length=50)
    charge = models.CharField(max_length=50)
    apartament = models.CharField(max_length=50)
    class Meta:
        db_table = 'EMPLEADO'

class Reserva(models.Model):
    user = models.ForeignKey(User, on_delete=models.CASCADE, default=1)
    fecha_reserva = models.DateField(default=timezone.now)
    nombre = models.CharField(max_length=50, default='Nombre')
    apellido = models.CharField(max_length=50, default='Apellido')
    cantidad_comensales = models.IntegerField()
    estado_reserva = models.CharField(max_length=20)
    tiempo = models.TimeField(default='00:00')
    comentario = models.CharField(max_length=200)
    mesa = models.ForeignKey(Mesa, on_delete=models.CASCADE, default=1)
    class Meta:
        db_table = 'RESERVA'

class Proveedor(models.Model):
    id = models.BigIntegerField(primary_key=True)
    nombre = models.CharField(max_length=50)
    email = models.CharField(max_length=50, blank=True, null=True)
    direccion = models.CharField(max_length=50)

    class Meta:
        db_table = 'proveedor'

class UnidadDeMedida(models.Model):
    id_u_medida = models.BigIntegerField(primary_key=True)
    codigo_u_medida = models.CharField(max_length=25)
    descripcion = models.CharField(max_length=25)

    class Meta:
        db_table = 'unidad_de_medida'

class TipoProducto(models.Model):
    id_tipo = models.BigIntegerField(primary_key=True)
    tipo_producto = models.CharField(max_length=25)

    class Meta:
        db_table = 'tipo_producto'

class Menu(models.Model):
    fecha = models.DateField(default=datetime.now)
    plato = models.CharField(max_length=20)
    descripcion = models.CharField(max_length=200)
    imagen = models.ImageField(upload_to="Menu", null=True)
    tipo_menu = models.ForeignKey(Tipo_menu, on_delete=models.CASCADE)
    class Meta:
        db_table = 'MENU'

class Receta(models.Model):
    id_receta = models.BigIntegerField(primary_key=True)
    nombre = models.CharField(max_length=25)
    descripcion = models.CharField(max_length=200)
    tiempo_preparacion = models.BigIntegerField()
    precio = models.FloatField()
    foto = models.BinaryField(blank=True, null=True)

    class Meta:
        db_table = 'receta'

class Producto(models.Model):
    id_producto = models.BigIntegerField(primary_key=True)
    descripcion = models.CharField(max_length=150)
    precio_compra = models.FloatField()
    precio_venta = models.FloatField()
    nombre = models.CharField(max_length=100)
    marca = models.CharField(max_length=25, blank=True, null=True)
    categoria = models.CharField(max_length=25)
    stock = models.BigIntegerField()
    fecha_vencimiento = models.DateField()
    unidad_de_medida_id_u_medida = models.ForeignKey(UnidadDeMedida, models.DO_NOTHING, related_name='unidad_de_medida_id_u_medida')
    tipo_producto_id_tipo = models.ForeignKey(TipoProducto, models.DO_NOTHING, related_name='tipo_producto_id_tipo', default=1)
    proveedor = models.ForeignKey(Proveedor, models.DO_NOTHING, default=1)

    class Meta:
        db_table = 'producto'
        unique_together = (('id_producto', 'unidad_de_medida_id_u_medida', 'tipo_producto_id_tipo'),)

class TipoDePago(models.Model):
    id_tipo_pago = models.BigIntegerField(primary_key=True)
    descripcion = models.CharField(max_length=25)

    class Meta:
        db_table = 'tipo_de_pago'

class Pedido(models.Model):
    id_pedido = models.BigIntegerField(primary_key=True)
    fecha_hora = models.DateTimeField()
    total = models.BigIntegerField()
    propina = models.BigIntegerField(blank=True, null=True)
    mesa_id_mesa = models.ForeignKey(Mesa, related_name='mesa_id_mesa', on_delete=models.CASCADE)
    class Meta:
        db_table = 'pedido'

class Pago(models.Model):
    id_pago = models.BigIntegerField(primary_key=True)
    fecha_pago = models.DateField(blank=True, null=True)
    url = models.CharField(max_length=500, blank=True, null=True)
    token = models.CharField(max_length=500, blank=True, null=True)
    orden_pago = models.BigIntegerField(blank=True, null=True)
    tipo_de_pago_id_tipo_pago = models.ForeignKey(TipoDePago, models.DO_NOTHING, db_column='tipo_de_pago_id_tipo_pago')
    cuotas = models.BigIntegerField(blank=True, null=True)
    pedido_id_pedido = models.ForeignKey(Pedido, models.DO_NOTHING, db_column='pedido_id_pedido')

    class Meta:
        db_table = 'pago'

class EstPedido(models.Model):
    id_est_pedido = models.BigIntegerField(primary_key=True)
    descripcion = models.CharField(max_length=25)

    class Meta:
        db_table = 'est_pedido'

class DetallePedido(models.Model):
    user_id_usuario = models.ForeignKey(User, models.DO_NOTHING, related_name='user_id_usuario', blank=True, null=True)
    descripcion = models.CharField(max_length=100)
    cantidad = models.BigIntegerField()
    pedido_id_pedido = models.OneToOneField(Pedido, models.DO_NOTHING, related_name='pedido_id_pedido', primary_key=True)
    receta_id_receta = models.ForeignKey(Receta, models.DO_NOTHING, related_name='receta_id_receta', blank=True, null=True)
    producto_id_producto = models.ForeignKey(Producto, models.DO_NOTHING, related_name='producto_id_producto', blank=True, null=True)
    producto_id_u_medida = models.ForeignKey(Producto, models.DO_NOTHING, related_name='producto_id_u_medida', blank=True, null=True)
    producto_id_tipo = models.ForeignKey(Producto, models.DO_NOTHING, related_name='producto_id_tipo', blank=True, null=True)
    est_pedido_id_est_pedido = models.ForeignKey(EstPedido, models.DO_NOTHING, related_name='est_pedido_id_est_pedido')

    class Meta:
        db_table = 'detalle_pedido'

class UsuarioPedido(models.Model):
    comentario_especial = models.CharField(max_length=25, blank=True, null=True)
    rol_id_rol = models.ForeignKey(Rol, models.DO_NOTHING, related_name='pedidos_asignados')
    pedido_id_pedido = models.ForeignKey(Pedido, models.DO_NOTHING, related_name='pedidos')
    class Meta:
        db_table = 'usuario_pedido'
        
class MensajeContacto(models.Model):
    nombre = models.CharField(max_length=50, verbose_name='Nombre')
    correo_electronico = models.CharField(max_length=50, verbose_name='Correo Electrónico')
    asunto = models.CharField(max_length=50, verbose_name='Asunto')
    mensaje = models.CharField(max_length=500, verbose_name='Mensaje')

    class Meta:
        db_table = 'MENSAJE_CONTACTO'
        verbose_name = 'Mensaje de Contacto'
