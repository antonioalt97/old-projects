from django.contrib.auth.models import User, Group
from django.db.models.signals import post_save
from django.dispatch import receiver
from home.models import Cliente

@receiver(post_save, sender=User)
def create_cliente_profile(sender, instance, created, **kwargs):
    if created:
        # Asociar al grupo 'Clientes' si el usuario es recién creado
        grupo_clientes, _ = Group.objects.get_or_create(name='Clientes')
        instance.groups.add(grupo_clientes)
        
        # Crear un perfil de cliente para el nuevo usuario
        Cliente.objects.get_or_create(user=instance, defaults={
            'adress': 'Dirección por defecto',
            'region': 'Región por defecto',
            'comuna': 'Comuna por defecto',
            'phone': 'Teléfono por defecto'
        })

        
