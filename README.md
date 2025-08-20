# APITestAutomation

## OpenAPI Test Generator

Este framework ahora incluye un generador automático de tests basado en especificaciones OpenAPI (Swagger).

### Características

- **Importación automática** de archivos JSON/YAML de OpenAPI
- **Generación automática** de tests para todos los endpoints
- **Detección de cambios** automática cuando se actualiza la especificación
- **Validaciones incluidas**:
  - Status codes esperados
  - Schema validation
  - Tests de paths negativos (unauthorized, missing parameters)
  - Response validation

### Uso

#### 1. Generar tests desde una especificación OpenAPI

```bash
dotnet run --project APITestAutomation generate swagger.json ptpd68r3nke7q5pnutzaaw PPSAutoTestUser0
```

#### 2. Detectar cambios en la especificación

```bash
dotnet run --project APITestAutomation detect swagger.json
```

#### 3. Preview de tests que se generarían

```bash
dotnet run --project APITestAutomation preview swagger.json ptpd68r3nke7q5pnutzaaw PPSAutoTestUser0
```

### Flujo de trabajo

1. **Primera vez**: Ejecuta `generate` para crear los tests iniciales
2. **Actualizaciones**: Ejecuta `detect` para ver qué cambios hay
3. **Aplicar cambios**: Ejecuta `generate` nuevamente, el sistema te preguntará si quieres aplicar los cambios
4. **Reporte**: Se genera automáticamente un reporte con todos los cambios aplicados

### Estructura de archivos generados

- `APITestAutomationTest/Generated/` - Tests generados automáticamente
- `APITestAutomation/Config/OpenAPI/` - Configuraciones y especificaciones guardadas
- `APITestAutomation/Reports/` - Reportes de cambios aplicados
- `APITestAutomation/Examples/` - Ejemplos de especificaciones OpenAPI

### Ejemplo de especificación OpenAPI

Puedes encontrar un ejemplo en `APITestAutomation/Examples/sample-openapi.json`

### Tests generados automáticamente

Para cada endpoint se generan:
- **Test positivo**: Verifica que el endpoint funciona correctamente
- **Test de autorización**: Verifica que endpoints protegidos requieren autenticación
- **Test de parámetros requeridos**: Verifica validación de parámetros obligatorios
- **Test de validación de schema**: Verifica que la respuesta cumple con el schema definido