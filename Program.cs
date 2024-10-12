using NUnit.Framework;

// Clase para los vasos
public class Vasos
{
    private Dictionary<int, int> cantidadVasosDisponibles = new Dictionary<int, int>
    {
        { 1, 10 }, // Vaso pequeño
        { 2, 15 }, // Vaso mediano
        { 3, 20 }  // Vaso grande
    };

    public bool EsVasoDisponible(int tamanoVaso)
    {
        return cantidadVasosDisponibles.ContainsKey(tamanoVaso) && cantidadVasosDisponibles[tamanoVaso] > 0;
    }

    public void DescontarVaso(int tamanoVaso)
    {
        if (EsVasoDisponible(tamanoVaso))
        {
            cantidadVasosDisponibles[tamanoVaso]--;
        }
    }

    public void AgregarVasos(int tamanoVaso, int cantidad)
    {
        if (cantidadVasosDisponibles.ContainsKey(tamanoVaso))
        {
            cantidadVasosDisponibles[tamanoVaso] += cantidad;
        }
    }

    public string MostrarCantidadVasos()
    {
        return $"Vasos disponibles: Pequeños ({cantidadVasosDisponibles[1]}), Medianos ({cantidadVasosDisponibles[2]}), Grandes ({cantidadVasosDisponibles[3]})";
    }
}

// Clase para la cafetera
public class Cafetera
{
    private int cantidadCafeDisponible = 30; // Cantidad de café inicial

    public bool EsCantidadCafeSuficiente(int cantidadCafe)
    {
        return cantidadCafeDisponible >= cantidadCafe;
    }

    public void DescontarCafe(int cantidadCafe)
    {
        if (EsCantidadCafeSuficiente(cantidadCafe))
        {
            cantidadCafeDisponible -= cantidadCafe;
        }
    }

    public void AgregarCafe(int cantidad)
    {
        cantidadCafeDisponible += cantidad;
    }

    public int MostrarCantidadCafe()
    {
        return cantidadCafeDisponible;
    }
}

// Clase para el azucarero
public class Azucarero
{
    private Dictionary<int, int> cantidadAzucarDisponible = new Dictionary<int, int>
    {
        { 1, 50 }, 
        { 2, 75 }, 
        { 3, 100 } 
    };

    public bool EsCantidadAzucarSuficiente(int cantidadAzucar, int tamanoVaso)
    {
        return cantidadAzucarDisponible.ContainsKey(tamanoVaso) && cantidadAzucarDisponible[tamanoVaso] >= cantidadAzucar;
    }

    public int ObtenerCantidadAzucarDisponible(int tamanoVaso)
    {
        return cantidadAzucarDisponible.ContainsKey(tamanoVaso) ? cantidadAzucarDisponible[tamanoVaso] : 0;
    }

    public void DescontarAzucar(int cantidadAzucar, int tamanoVaso)
    {
        if (EsCantidadAzucarSuficiente(cantidadAzucar, tamanoVaso))
        {
            cantidadAzucarDisponible[tamanoVaso] -= cantidadAzucar;
        }
    }

    public void AgregarAzucar(int tamanoVaso, int cantidad)
    {
        if (cantidadAzucarDisponible.ContainsKey(tamanoVaso))
        {
            cantidadAzucarDisponible[tamanoVaso] += cantidad;
        }
    }

    public string MostrarCantidadAzucar()
    {
        return $"Azúcar disponible: Pequeños ({cantidadAzucarDisponible[1]} cucharadas), Medianos ({cantidadAzucarDisponible[2]} cucharadas), Grandes ({cantidadAzucarDisponible[3]} cucharadas)";
    }
}

public class MaquinaDeCafe
{
    public Vasos vasos;
    public Cafetera cafetera;
    public Azucarero azucarero;

    public MaquinaDeCafe(Vasos vasos, Cafetera cafetera, Azucarero azucarero)
    {
        this.vasos = vasos;
        this.cafetera = cafetera;
        this.azucarero = azucarero;
    }

    public string PrepararCafe(int tamanoVaso, int cantidadCafe, int cantidadAzucar)
    {
        if (!vasos.EsVasoDisponible(tamanoVaso))
        {
            return "Vaso no disponible";
        }

        if (!cafetera.EsCantidadCafeSuficiente(cantidadCafe))
        {
            return "No hay suficiente café";
        }

        if (!azucarero.EsCantidadAzucarSuficiente(cantidadAzucar, tamanoVaso))
        {
            return "No hay suficiente azúcar";
        }

        vasos.DescontarVaso(tamanoVaso);
        cafetera.DescontarCafe(cantidadCafe);
        azucarero.DescontarAzucar(cantidadAzucar, tamanoVaso);

        return $"Vaso de café (tamaño {tamanoVaso}) con {cantidadAzucar} cucharadas de azúcar";
    }

    public string MostrarInventario()
    {
        return $"{vasos.MostrarCantidadVasos()}\nCafé disponible: {cafetera.MostrarCantidadCafe()} oz\n{azucarero.MostrarCantidadAzucar()}";
    }
}

class Program
{
    static void Main()
    {
        // Crear instancias de las clases
        Vasos vasos = new Vasos();
        Cafetera cafetera = new Cafetera();
        Azucarero azucarero = new Azucarero();

        // Crear la máquina de café
        MaquinaDeCafe maquinaDeCafe = new MaquinaDeCafe(vasos, cafetera, azucarero);

        // Menú principal
        while (true)
        {
            Console.Clear();
            Console.WriteLine("¡Bienvenido a la Máquina de Café!");
            Console.WriteLine("1. Preparar café");
            Console.WriteLine("2. Mostrar inventario");
            Console.WriteLine("3. Agregar café");
            Console.WriteLine("4. Agregar azúcar");
            Console.WriteLine("5. Agregar vasos");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");

            if (int.TryParse(Console.ReadLine(), out int opcion))
            {
                switch (opcion)
                {
                    case 1:
                        Console.Clear();
                        PrepararCafe(maquinaDeCafe);
                        break;
                    case 2:
                        Console.Clear();
                        MostrarInventario(maquinaDeCafe);
                        break;
                    case 3:
                        Console.Clear();
                        AgregarCafe(maquinaDeCafe);
                        break;
                    case 4:
                        Console.Clear();
                        AgregarAzucar(maquinaDeCafe);
                        break;
                    case 5:
                        Console.Clear();
                        AgregarVasos(maquinaDeCafe);
                        break;
                    case 0:
                        Console.WriteLine("Gracias por usar la Máquina de Café. ¡Hasta luego!");
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Opción no válida. Inténtelo de nuevo.");
                        break;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Por favor, ingrese un número válido.");
            }
        }
    }

    static void PrepararCafe(MaquinaDeCafe maquinaDeCafe)
    {
        Console.WriteLine("\nSeleccione el tamaño del vaso:");
        Console.WriteLine("1. Pequeño");
        Console.WriteLine("2. Mediano");
        Console.WriteLine("3. Grande");

        Console.Write("Seleccione una opción: ");

        if (int.TryParse(Console.ReadLine(), out int tamanoVaso) && tamanoVaso >= 1 && tamanoVaso <= 3)
        {
            int maxCantidadCafe = tamanoVaso == 1 ? 10 : tamanoVaso == 2 ? 15 : 20;

            Console.Write($"Ingrese la cantidad de café (en Oz, máximo {maxCantidadCafe}): ");
            if (int.TryParse(Console.ReadLine(), out int cantidadCafe) && cantidadCafe > 0 && cantidadCafe <= maxCantidadCafe)
            {
                int maxCantidadAzucar = maquinaDeCafe.azucarero.ObtenerCantidadAzucarDisponible(tamanoVaso);

                Console.Write($"Ingrese la cantidad de azúcar (en cucharadas, máximo {maxCantidadAzucar}): ");
                if (int.TryParse(Console.ReadLine(), out int cantidadAzucar) && cantidadAzucar >= 0 && cantidadAzucar <= maxCantidadAzucar)
                {
                    string resultado = maquinaDeCafe.PrepararCafe(tamanoVaso, cantidadCafe, cantidadAzucar);
                    Console.WriteLine(resultado);
                    Console.WriteLine("Presione Enter para volver al menú principal...");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Cantidad de azúcar inválida.");
                }
            }
            else
            {
                Console.WriteLine("Cantidad de café inválida.");
            }
        }
        else
        {
            Console.WriteLine("Opción no válida.");
        }
    }

    static void MostrarInventario(MaquinaDeCafe maquinaDeCafe)
    {
        Console.WriteLine("\nINVENTARIO:");
        Console.WriteLine(maquinaDeCafe.MostrarInventario());
        Console.WriteLine("Presione Enter para volver al menú principal...");
        Console.ReadLine();
    }

    static void AgregarCafe(MaquinaDeCafe maquinaDeCafe)
    {
        Console.Write("\nIngrese la cantidad de café (en Oz) a agregar: ");
        if (int.TryParse(Console.ReadLine(), out int cantidadCafe) && cantidadCafe > 0)
        {
            maquinaDeCafe.cafetera.AgregarCafe(cantidadCafe);
            Console.WriteLine($"Se agregaron {cantidadCafe} Oz de café.");
        }
        else
        {
            Console.WriteLine("Cantidad inválida.");
        }
        Console.WriteLine("Presione Enter para volver al menú principal...");
        Console.ReadLine();
    }

    static void AgregarAzucar(MaquinaDeCafe maquinaDeCafe)
    {
        Console.WriteLine("\nSeleccione el tamaño del vaso para agregar azúcar:");
        Console.WriteLine("1. Pequeño");
        Console.WriteLine("2. Mediano");
        Console.WriteLine("3. Grande");

        if (int.TryParse(Console.ReadLine(), out int tamanoVaso) && tamanoVaso >= 1 && tamanoVaso <= 3)
        {
            Console.Write($"Ingrese la cantidad de azúcar (en cucharadas) a agregar para los vasos de tamaño {tamanoVaso}: ");
            if (int.TryParse(Console.ReadLine(), out int cantidadAzucar) && cantidadAzucar > 0)
            {
                maquinaDeCafe.azucarero.AgregarAzucar(tamanoVaso, cantidadAzucar);
                Console.WriteLine($"Se agregaron {cantidadAzucar} cucharadas de azúcar.");
            }
            else
            {
                Console.WriteLine("Cantidad inválida.");
            }
        }
        else
        {
            Console.WriteLine("Opción no válida.");
        }

        Console.WriteLine("Presione Enter para volver al menú principal...");
        Console.ReadLine();
    }

    static void AgregarVasos(MaquinaDeCafe maquinaDeCafe)
    {
        Console.WriteLine("\nSeleccione el tamaño del vaso para agregar:");
        Console.WriteLine("1. Pequeño");
        Console.WriteLine("2. Mediano");
        Console.WriteLine("3. Grande");

        if (int.TryParse(Console.ReadLine(), out int tamanoVaso) && tamanoVaso >= 1 && tamanoVaso <= 3)
        {
            Console.Write($"Ingrese la cantidad de vasos a agregar para el tamaño {tamanoVaso}: ");
            if (int.TryParse(Console.ReadLine(), out int cantidadVasos) && cantidadVasos > 0)
            {
                maquinaDeCafe.vasos.AgregarVasos(tamanoVaso, cantidadVasos);
                Console.WriteLine($"Se agregaron {cantidadVasos} vasos de tamaño {tamanoVaso}.");
            }
            else
            {
                Console.WriteLine("Cantidad inválida.");
            }
        }
        else
        {
            Console.WriteLine("Opción no válida.");
        }

        Console.WriteLine("Presione Enter para volver al menú principal...");
        Console.ReadLine();
    }

    [TestFixture]
    public class MaquinaDeCafeTests
    {
        private Vasos vasos;
        private Cafetera cafetera;
        private Azucarero azucarero;
        private MaquinaDeCafe maquinaDeCafe;

        [SetUp]
        public void SetUp()
        {
            vasos = new Vasos();
            cafetera = new Cafetera();
            azucarero = new Azucarero();
            maquinaDeCafe = new MaquinaDeCafe(vasos, cafetera, azucarero);
        }

        [Test]
        public void PrepararCafe_VasoPequeno_SuficienteCafeAzucar_RetornaMensaje()
        {
            // Arrange
            int tamanoVaso = 1; // Vaso pequeño
            int cantidadCafe = 5;
            int cantidadAzucar = 2;

            // Act
            string resultado = maquinaDeCafe.PrepararCafe(tamanoVaso, cantidadCafe, cantidadAzucar);

            // Assert
            Assert.Equals($"Vaso de café (tamaño {tamanoVaso}) con {cantidadAzucar} cucharadas de azúcar", resultado);
        }

        [Test]
        public void PrepararCafe_VasoMediano_NoSuficienteCafe_RetornaMensajeNoCafe()
        {
            // Arrange
            int tamanoVaso = 2; // Vaso mediano
            int cantidadCafe = 20; // Más café del disponible

            // Act
            string resultado = maquinaDeCafe.PrepararCafe(tamanoVaso, cantidadCafe, 0);

            // Assert
            Assert.Equals("No hay suficiente café", resultado);
        }

        [Test]
        public void PrepararCafe_VasoGrande_NoSuficienteAzucar_RetornaMensajeNoAzucar()
        {
            // Arrange
            int tamanoVaso = 3; // Vaso grande
            int cantidadCafe = 10;
            int cantidadAzucar = 150; // Más azúcar del disponible

            // Act
            string resultado = maquinaDeCafe.PrepararCafe(tamanoVaso, cantidadCafe, cantidadAzucar);

            // Assert
            Assert.Equals("No hay suficiente azúcar", resultado);
        }

        [Test]
        public void PrepararCafe_VasoNoDisponible_RetornaMensajeNoVaso()
        {
            // Arrange
            int tamanoVaso = 4; // Tamaño no existente

            // Act
            string resultado = maquinaDeCafe.PrepararCafe(tamanoVaso, 5, 0);

            // Assert
            Assert.Equals("Vaso no disponible", resultado);
        }
    }

}
