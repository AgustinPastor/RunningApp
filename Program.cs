using System;
using System.Collections.Generic;
using System.Linq;

class Usuario
{
    public string Email { get; }
    private string Contraseña;
    public List<Entrenamiento> Entrenamientos { get; }

    public Usuario(string email, string contraseña)
    {
        Email = email;
        Contraseña = contraseña;
        Entrenamientos = new List<Entrenamiento>();
    }

    public bool VerificarContraseña(string contraseña)
    {
        return Contraseña == contraseña;
    }
}

class Entrenamiento
{
    public double Distancia { get; }
    public double Tiempo { get; }

    public Entrenamiento(double distancia, double tiempo)
    {
        Distancia = distancia;
        Tiempo = tiempo;
    }

    public override string ToString()
    {
        return $"Distancia: {Distancia} km - Tiempo: {Tiempo} min";
    }
}

class SistemaEntrenamiento
{
    private List<Usuario> usuarios;
    private Usuario usuarioActual;

    public SistemaEntrenamiento()
    {
        usuarios = new List<Usuario>();
        usuarioActual = null;
    }

    public void RegistrarUsuario()
    {
        Console.Write("Introduce tu email: ");
        string email = Console.ReadLine();

        if (usuarios.Any(u => u.Email == email))
        {
            Console.WriteLine("Este email ya está registrado.");
            return;
        }

        Console.Write("Introduce tu contraseña: ");
        string contraseña = Console.ReadLine();

        usuarios.Add(new Usuario(email, contraseña));
        Console.WriteLine("Usuario registrado con éxito.");
    }

    public void IniciarSesion()
    {
        Console.Write("Email: ");
        string email = Console.ReadLine();
        Console.Write("Contraseña: ");
        string contraseña = Console.ReadLine();

        Usuario usuario = usuarios.FirstOrDefault(u => u.Email == email && u.VerificarContraseña(contraseña));

        if (usuario != null)
        {
            usuarioActual = usuario;
            Console.WriteLine("Inicio de sesión exitoso.");
            MenuUsuario();
        }
        else
        {
            Console.WriteLine("Email o contraseña incorrectos.");
        }
    }

    private void MenuUsuario()
    {
        int opcion;
        do
        {
            Console.WriteLine("\n1. Registrar entrenamiento");
            Console.WriteLine("2. Listar entrenamientos");
            Console.WriteLine("3. Vaciar entrenamientos");
            Console.WriteLine("4. Cerrar sesión");
            Console.Write("Selecciona una opción: ");

            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("Opción inválida.");
                continue;
            }

            switch (opcion)
            {
                case 1:
                    RegistrarEntrenamiento();
                    break;
                case 2:
                    ListarEntrenamientos();
                    break;
                case 3:
                    VaciarEntrenamientos();
                    break;
                case 4:
                    usuarioActual = null;
                    Console.WriteLine("Sesión cerrada.");
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        } while (opcion != 4);
    }

    private void RegistrarEntrenamiento()
    {
        Console.Write("Introduce la distancia (km): ");
        if (!double.TryParse(Console.ReadLine(), out double distancia))
        {
            Console.WriteLine("Valor inválido.");
            return;
        }

        Console.Write("Introduce el tiempo (min): ");
        if (!double.TryParse(Console.ReadLine(), out double tiempo))
        {
            Console.WriteLine("Valor inválido.");
            return;
        }

        usuarioActual.Entrenamientos.Add(new Entrenamiento(distancia, tiempo));
        Console.WriteLine("Entrenamiento registrado.");
    }

    private void ListarEntrenamientos()
    {
        if (usuarioActual.Entrenamientos.Count == 0)
        {
            Console.WriteLine("No hay entrenamientos registrados.");
            return;
        }

        Console.WriteLine("Entrenamientos:");
        foreach (var entrenamiento in usuarioActual.Entrenamientos)
        {
            Console.WriteLine(entrenamiento);
        }
    }

    private void VaciarEntrenamientos()
    {
        usuarioActual.Entrenamientos.Clear();
        Console.WriteLine("Lista de entrenamientos vaciada.");
    }
}

class Program
{
    static void Main()
    {
        SistemaEntrenamiento sistema = new SistemaEntrenamiento();
        int opcion;

        do
        {
            Console.WriteLine("\n1. Registrar usuario");
            Console.WriteLine("2. Iniciar sesión");
            Console.WriteLine("3. Salir");
            Console.Write("Selecciona una opción: ");

            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("Opción inválida.");
                continue;
            }

            switch (opcion)
            {
                case 1:
                    sistema.RegistrarUsuario();
                    break;
                case 2:
                    sistema.IniciarSesion();
                    break;
                case 3:
                    Console.WriteLine("Saliendo...");
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        } while (opcion != 3);
    }
}
