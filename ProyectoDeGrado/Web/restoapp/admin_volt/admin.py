from django.contrib import admin
from home.models import Cliente, Reserva
from django.contrib.auth.models import User
from home.models import Proveedor, Producto, Menu, Tipo_menu, Mesa, Reserva, Cliente, Receta, Producto, Tipo_menu, Mesa, DetallePedido, EstPedido, Pedido, MensajeContacto as Contact, Rol
from django.contrib.auth.admin import UserAdmin as BaseUserAdmin

admin.site.unregister(User)  # Desregistra el UserAdmin por defecto

class UserAdmin(BaseUserAdmin):
    fieldsets = (
        (None, {'fields': ('username', 'password')}),
        (('Personal info'), {'fields': ('first_name', 'last_name', 'email')}),
        (('Permissions'), {'fields': ('is_active', 'is_staff', 'is_superuser', 'groups', 'user_permissions')}),
        (('Important dates'), {'fields': ('last_login', 'date_joined')}),
    )
    add_fieldsets = (
        (None, {
            'classes': ('wide',),
            'fields': ('username', 'password1', 'password2', 'is_active', 'is_staff', 'is_superuser', 'groups'),
        }),
    )
    filter_horizontal = ('groups', 'user_permissions',)

class ProveedorAdmin(admin.ModelAdmin):
    list_display = ["nombre", "email", "direccion"]
    search_fields = ['id', 'nombre', 'email', 'direccion']
    list_per_page = 4

class ReservaAdmin(admin.ModelAdmin):
    list_display = ('id', 'get_username', 'fecha_reserva', 'cantidad_comensales', 'estado_reserva', 'tiempo', 'comentario', 'mesa')
    search_fields = ('id', 'user__username', 'user__email', 'fecha_reserva', 'cantidad_comensales', 'estado_reserva', 'tiempo', 'comentario', 'mesas') 
    readonly_fields = ['user']
    list_per_page = 10

    def get_username(self, obj):
        return obj.user.username
    
    get_username.short_description = 'Nombre de usuario'


class ClienteAdmin(admin.ModelAdmin):
    list_display = ('id', 'get_username', 'adress', 'region', 'comuna', 'phone')
    search_fields = ('id', 'user__username', 'user__email', 'adress', 'phone') 
    list_per_page = 4

    def get_username(self, obj):
        return obj.user.username
    
    get_username.short_description = 'Nombre de usuario'

class ProductoAdmin(admin.ModelAdmin):
    list_display = ['nombre', 'marca', 'categoria', 'precio_compra', 'precio_venta']
    search_fields = ['id', 'nombre', 'marca', 'categoria', 'precio_compra', 'precio_venta']
    list_per_page = 4

class MesaAdmin(admin.ModelAdmin):
    list_display = ["numero_mesa", "capacidad", "descripcion", "en_uso", "activa"]
    search_fields = ['id', 'numero_mesa', 'capacidad', 'descripcion', 'en_uso', 'activa']
    list_per_page = 4
    class Meta:
        db_table = 'MESA'

class MenuAdmin(admin.ModelAdmin):
    list_display = ('get_fecha', 'plato', 'descripcion', 'tipo_menu')
    # Buscamos el objeto de tipo menu ya que es una relacion
    search_fields = ('id', 'fecha', 'plato', 'descripcion', 'tipo_menu.tipo_menu')
    
    def get_fecha(self, obj):
        return obj.fecha.strftime("%Y-%m-%d")
    get_fecha.short_description = 'Fecha'
    
class Tipo_menuAdmin(admin.ModelAdmin):
    list_display = ["tipo_menu"]

class EstPedidoAdmin(admin.ModelAdmin):
    list_display = ["descripcion"]

class RolAdmin(admin.ModelAdmin):
    list_display = ["id_rol", "descripcion"]

class ContactAdmin(admin.ModelAdmin):
    list_display = ["nombre", "correo_electronico", "asunto", "mensaje"]
    search_fields = ['id', 'nombre', 'email', 'asunto', 'mensaje']
    list_per_page = 4

admin.site.register(Rol, RolAdmin)
admin.site.register(Cliente, ClienteAdmin)
admin.site.register(User, UserAdmin)
admin.site.register(Reserva, ReservaAdmin)
admin.site.register(Proveedor, ProveedorAdmin)
admin.site.register(Producto, ProductoAdmin)
admin.site.register(Menu, MenuAdmin)
admin.site.register(Tipo_menu, Tipo_menuAdmin)
admin.site.register(Mesa, MesaAdmin)
admin.site.register(DetallePedido)
admin.site.register(Receta)
admin.site.register(EstPedido, EstPedidoAdmin)
admin.site.register(Pedido)
admin.site.register(Contact, ContactAdmin)
