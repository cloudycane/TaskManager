# Task Manager ERP / Plataforma de Gestión de Reservas, Pedidos y Fidelización para Restaurantes

<b>Proyecto de conocimiento: </b> Un restaurante llamado "Resto Fibonacci" necesitaría un plan de recurso empresarial Software as a Service en la nube (cloud-based SaaS ERP) simple y ágil que gestiona todas las tareas diarias y las distintas actividades de negocios (adquisición, inventario, producción, pedidos, ventas, contabilidad, etc.).

https://github.com/user-attachments/assets/a6a09f1f-0259-4289-a8a1-dc7546ee5b46

Abajo se puede visualizar las actividades de negocio dentro del establecimiento: 
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

https://github.com/user-attachments/assets/0ea2374a-4927-4258-9c62-aa48407018d8

Este módulo ayuda al restaurante en la manera que registra los posibles suministradores (sourcing) para la adquisición de los productos necesarios para el inventario. El producto adquirido se convertirá a "materia prima" para la producción. Este portal de los suministradores permite registrar, organizar, editar, y eliminar los datos empresariales de los suministradores incluso generar archivos tales como CSV y Excel (xlsx). 

## Módulo 2: Productos de los Suministradores 

https://github.com/user-attachments/assets/513f9559-0d20-44de-9465-2a6d24c6da66

En esta área podemos ver el catálogo o el listado de los productos en venta de los suministradores registrados en nuestra aplicación. Tiene funciones como la de crear, organizar, editar, eliminar, y ordenar los productos incluso generar archivos CSV y Excel (xlsx).  

## Módulo 3: Ordénes de los Productos de los Suministradores 

https://github.com/user-attachments/assets/8feb6f81-b1b7-41e3-a3bd-32fd4dfcd2c0

Si deseamos ordenar un producto de los suministradores, aquí es el siguiente destino. Esta sección nos mostrará un detalle de nuestros pedidos tales como las cantidades, el proveedor, el precio total, la fecha de orden, etc. Tambiém tiene funciones de crear, organizar, editar, eliminar, y comprar los ordenes incluso generar archivos CSV y Excel (xlsx).

## Módulo 4: Compra / Adquisición de las Materias Primas 

https://github.com/user-attachments/assets/6c4add4f-675d-4a51-a910-42e6710a26a1


En este proceso, realizamos el pago de los productos que hemos ordenado a los suministradores. Asegura la obtención de las materias primas necesarias de manera oportuna y eficiente. Este módulo es vital para garantizar un flujo constante de materiales, optimizando costos y asegurando la continuidad de la producción.

## Módulo 5: Inventario de Materias Primas 


https://github.com/user-attachments/assets/db2f173a-3a64-46d4-b19d-d551d6dead07


Este módulo se encarga de la gestión del inventario de materias primas, asegurando que se mantengan niveles adecuados para evitar interrupciones en la producción. Incluye el monitoreo constante de existencias, control de entradas y salidas, y la implementación de estrategias para minimizar pérdidas por obsolescencia o deterioro.

## Módulo 6: Reservación de Cliente



https://github.com/user-attachments/assets/8b2cf605-b00a-4447-bdd2-abb3d50c7f32



Es una de las características primordiales del proyecto ya que no solamente ayuda a los clientes a reservar su puesto para el restaurante si no también ayuda a la empresa a gestionar las reservaciones de muchas clientes día a día. Este módulo permite crear, organizar, editar, eliminar, generar archivos CSV y Excel (xlsx) de las reservaciones incluso hacer un recordatorio y una validación del acceso en formato PDF. 

## Módulo 7: Gestión de los Pedidos de los Clientes 



https://github.com/user-attachments/assets/7edc3245-d253-4549-9335-00c4e8398dec



Este módulo cubre todo el ciclo de vida de los pedidos de los clientes, desde la recepción y confirmación hasta el envío y entrega. Su objetivo es garantizar que los pedidos se procesen de manera eficiente y puntual, mejorando la experiencia del cliente y fortaleciendo la relación comercial. Una vez que los clientes han realizado sus pedidos (en línea o dentro del restaurante), la aplicación notificará y mostrará los detalles de sus pedidos a los empleados y iniciará el proceso de la producción. En esta fase se está teniendo en cuenta las cantidades de los productos en el inventario. Cada vez que se produce algo, se reduce la cantidad de cada uno de los productos utilizados para hacer los pedidos del cliente.  


## Módulo 8: Creación de los Productos En Ventas



https://github.com/user-attachments/assets/e6902c93-450b-4b28-a189-89a64efd3e3f




## Módulo 9: Facturación/Pago (Cliente)

![ps9](https://github.com/user-attachments/assets/e3809c19-906c-41d8-9831-8df2a181deea)


En este proceso el cliente realiza el pago para la comida o los productos que ha pedido sea en línea o dentro del restaurante. 
