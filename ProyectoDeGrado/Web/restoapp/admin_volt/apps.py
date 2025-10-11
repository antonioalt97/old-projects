from django.apps import AppConfig


class AdminVoltConfig(AppConfig):
    default_auto_field = 'django.db.models.BigAutoField'
    name = 'admin_volt'
    def ready(self):
        import admin_volt.signals