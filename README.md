# Task Manager ERP / Plataforma de Gestión de Reservas, Pedidos y Fidelización para Restaurantes

<b>Proyecto de conocimiento: </b> Un restaurante llamado "Resto Fibonacci" necesitaría un plan de recurso empresarial Software as a Service en la nube (cloud-based SaaS ERP) simple y ágil que gestiona todas las tareas diarias y las distintas actividades de negocios (adquisición, inventario, producción, pedidos, ventas, contabilidad, etc.).

https://github.com/user-attachments/assets/a6a09f1f-0259-4289-a8a1-dc7546ee5b46

Abajo se puede visualizar las actividades de negocio dentro de la empresa o establecimiento: 
<ul>
  <li><b>Actividades de los clientes: </b>El gráfico empieza por el cliente al hacer su pedido online o al hacer su reservación para comer dentro del restaurante.</li>
  <li><b>Producción: </b>Tras realizar el pedido, la próxima actividad es la producción. El restaurante recibirá los detalles del pedido y los cocineros cocinan los que apetezcan los clientes.</li>
  <li><b>Adquisición de los requisitos del inventario: </b>los recursos para hacer el pedido de los clientes pueden abastecerse, por eso se notifica los requisitos y las necesidades del inventario, además se hace un "sourcing", es decir, buscar un suministrador y materias primas adecuadamente.</b></li>
  <li><b>Facturación, Contabilidad y Finanza: </b>se hace un seguimiento de los gastos (pago de la adquisición, renta, mantenimiento, etc.) y los ingresos (pago de los clientes, donaciones, etc.) a partir de las facturas emitidas </li>
  <li><b>Ventas y Marketing: </b> Gestión de los productos terminados para vender al público y publicidades para ganar visitas y nuevos clientes.</li>
  <li><b>Recursos Humanos (RRHH): </b> Gestión de los empleados (sea nuevos integrantes o trabajadores anteriores) y el salario.</li>
</ul>
<br>
<img src="./graph1.png" />

# Los Módulos y Flujo de Negocio (Modules And Business Process Flows) 
## Módulo 1: Portal de los Suministradores

https://github.com/user-attachments/assets/4e1b8366-fa8a-470f-9580-1bedf5624b69

Este módulo ayuda al restaurante en la manera que registra los posibles suministradores (sourcing) para la adquisición de los productos necesarios para el inventario. El producto adquirido se convertirá a "materia prima" para la producción. Este portal de los suministradores permite registrar, organizar, editar, y eliminar los datos empresariales de los suministradores incluso generar archivos tales como CSV y Excel (xlsx). 

## Módulo 2: Productos de los Suministradores 

En esta área podemos ver el catálogo o el listado de los productos en venta de los suministradores registrados en nuestra aplicación. Tiene funciones como la de crear, organizar, editar, eliminar, y ordenar los productos incluso generar archivos CSV y Excel (xlsx).  

## Módulo 3: Ordénes de los Productos de los Suministradores 

Si deseamos ordenar un producto de los suministradores, aquí es el siguiente destino. Esta sección nos mostrará un detalle de nuestros pedidos tales como las cantidades, el proveedor, el precio total, la fecha de orden, etc. Tambiém tiene funciones de crear, organizar, editar, eliminar, y comprar los ordenes incluso generar archivos CSV y Excel (xlsx).

## Módulo 4: Compra / Adquisición de las Materias Primas 

En este proceso, realizamos el pago de los productos que hemos ordenado a los suministradores. Asegura la obtención de las materias primas necesarias de manera oportuna y eficiente. Este módulo es vital para garantizar un flujo constante de materiales, optimizando costos y asegurando la continuidad de la producción.

## Módulo 5: Inventario de Materias Primas 

Este módulo se encarga de la gestión del inventario de materias primas, asegurando que se mantengan niveles adecuados para evitar interrupciones en la producción. Incluye el monitoreo constante de existencias, control de entradas y salidas, y la implementación de estrategias para minimizar pérdidas por obsolescencia o deterioro.

## Módulo 6: Reservación de Cliente

https://github.com/user-attachments/assets/c153edf8-5241-4b73-a0b1-ca8da3437b67

Es una de las características primordiales del proyecto ya que no solamente ayuda a los clientes a reservar su puesto para el restaurante si no también ayuda a la empresa a gestionar las reservaciones de muchas clientes día a día. Este módulo permite crear, organizar, editar, eliminar, generar archivos CSV y Excel (xlsx) de las reservaciones incluso hacer un recordatorio y una validación del acceso en formato PDF. 

## Módulo 7: Gestión de los Pedidos de los Clientes 

Este módulo cubre todo el ciclo de vida de los pedidos de los clientes, desde la recepción y confirmación hasta el envío y entrega. Su objetivo es garantizar que los pedidos se procesen de manera eficiente y puntual, mejorando la experiencia del cliente y fortaleciendo la relación comercial.

## Módulo 8: Producción / Producción de los Pedidos 

Una vez que los clientes han realizado sus pedidos (en línea o dentro del restaurante), la aplicación notificará y mostrará los detalles de sus pedidos a los empleados y iniciará el proceso de la producción. En esta fase se está teniendo en cuenta las cantidades de los productos en el inventario. Cada vez que se produce algo, se reduce la cantidad de cada uno de los productos utilizados para hacer los pedidos del cliente.  
## Módulo 9: Facturación/Pago (Cliente)

En este proceso el cliente realiza el pago para la comida o los productos que ha pedido. 
## Módulo 10: Inventario (Productos Terminados o Consumos)

Gestiona el inventario de productos terminados o bienes de consumo, asegurando que siempre haya suficiente stock para satisfacer la demanda sin incurrir en excesos. Este módulo también se encarga de optimizar el espacio de almacenamiento y mejorar la rotación de inventario para maximizar la eficiencia operativa.

## Módulo 11: Pedidos / Gestión de Pedidos de Clientes 

Coordina todos los aspectos relacionados con los pedidos de los clientes, desde la recepción hasta la entrega final. Incluye la comunicación con el cliente, la gestión de la logística y el monitoreo del estado de los pedidos para asegurar una entrega puntual y en las condiciones acordadas.

## Módulo 12: Ventas 

Este módulo abarca la gestión del proceso de ventas, desde la captación de clientes hasta el cierre de ventas. Incluye el análisis de mercado, la creación de estrategias de venta, la gestión de relaciones con los clientes, y el seguimiento de objetivos de ventas para maximizar los ingresos y la rentabilidad.

## Módulo 13: Contabilidad y Finanzas

Se centra en la administración financiera de la empresa, incluyendo el registro y análisis de todas las transacciones financieras. Este módulo proporciona informes financieros precisos y oportunos, esenciales para la toma de decisiones estratégicas, el cumplimiento fiscal y la gestión del presupuesto empresarial.
