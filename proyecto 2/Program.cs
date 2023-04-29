using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace proyecto_2
{
    internal class Program
    {
        static int filas = 6;
        static int columnas = 7;
        static int conecta = 4;
        static string Ficha1 = "o";
        static string Ficha2 = "x";
        static string EspacioVacio = " ";

        static int ErrorFilaInvalida = 24000;
        static int ErrorColumnaLlena = 24001;
        static int ErrorNinguno = 24002;
        static int ConectaArriba = 24003;
        static int ConectaDerecha = 24004;
        static int ConectaAbajoDerecha = 24005;
        static int ConectaArribaDerecha = 24006;
        static int NoConecta = 24007;
        static int FilaNoEncontrada = 24008;
        static string JugadorCpu = Ficha2;
        static int TurnosJugador1 = 0;
        static int TurnosJugador2 = 0;

        static void Main(string[] args)
        {
            int rep = 0;
            string jugador1, jugador2;
            string[] ganadores = new string[10];
            int ContadorPartidas = 0;
            Stopwatch tiempo= new Stopwatch();
            TimeSpan TiempoDeUso;
            do
            {
                Console.Clear();
                Console.WriteLine("BIENVENIDO!");
                Console.WriteLine("Escoge una opcion:");
                Console.WriteLine("1) 1 jugador");
                Console.WriteLine("2) 2 jugadores");
                Console.WriteLine("3) ultimas 10 partidas guardadas");
                Console.WriteLine("4) salir");
            int Menu = int.Parse(Console.ReadLine());

                switch (Menu)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Escriba el nombre del jugador (no puede ser computadora)");
                        jugador1 = Console.ReadLine();
                        string jugadorActual = "";
                        string[,] tablero = new string[filas, columnas];
                        string FichaActual = elegirJugadorAlAzar();
                        if (FichaActual == Ficha1)
                        {
                            jugadorActual = jugador1;
                        }
                        else
                        {
                            if (FichaActual == Ficha2)
                            {
                                jugadorActual = "COMPUTADORA";
                            }
                        }
                        if (jugador1 != "computadora")
                        {
                            TurnosJugador1 = 0;
                            tiempo.Restart();
                            tiempo.Start();

                            limpiarTablero(tablero);
                            Console.WriteLine("Comienza el jugador " + jugadorActual);
                            while (true)
                            {
                                int columna = 0;
                                Console.WriteLine("\nTurno del jugador " + jugadorActual);
                                dibujarTablero(tablero);


                                if (FichaActual == JugadorCpu)
                                {
                                    Console.Write("CPU 2 pensando...");
                                    columna = cpu();
                                }
                                else
                                {
                                    columna = solicitarColumnaAJugador();
                                    TurnosJugador1++;
                                }

                                int estado = colocarPieza(FichaActual, columna, tablero);
                                if (estado == ErrorColumnaLlena)
                                {
                                    Console.Write("Error: columna llena");
                                }
                                else if (estado == ErrorFilaInvalida)
                                {
                                    Console.Write("Fila no correcta");
                                }
                                else if (estado == ErrorNinguno)
                                {
                                    int g = ganador(FichaActual, tablero);
                                    if (g != NoConecta)
                                    {
                                        dibujarTablero(tablero);
                                        Console.Write("Gana el jugador " + jugadorActual + " en " + TurnosJugador1 + " turnos ");
                                        if (ganador(FichaActual, tablero) == ConectaArriba)
                                        {
                                            Console.WriteLine("conectando en vertical");
                                        }
                                        else
                                        {
                                            if (ganador(FichaActual, tablero) == ConectaDerecha)
                                            {
                                                Console.WriteLine("conectando en horizontal");
                                            }
                                            else
                                            {
                                                if (ganador(FichaActual, tablero) == ConectaAbajoDerecha || ganador(FichaActual, tablero) == ConectaArribaDerecha)
                                                {
                                                    Console.WriteLine("conectando en diagonal");
                                                }
                                            }
                                        }
                                        break;
                                    }
                                    else if (esEmpate(tablero))
                                    {
                                        dibujarTablero(tablero);
                                        Console.Write("Empate");
                                        break;
                                    }
                                }
                                if(estado != ErrorColumnaLlena && estado != ErrorFilaInvalida)
                                {

                                    FichaActual = obtenerOponente(FichaActual);
                                        if(FichaActual == Ficha1)
                                        {
                                            jugadorActual = jugador1;
                                        }
                                        else
                                        {
                                            if (FichaActual == Ficha2)
                                            {
                                                jugadorActual = "CPU";
                                            }
                                        }
                                }
                            }
                            tiempo.Stop();
                            TiempoDeUso = tiempo.Elapsed;
                            ganadores[ContadorPartidas] = "Gana el jugador " + jugadorActual + " en " + TurnosJugador1 + " turnos, tiempo: " + TiempoDeUso.Minutes + ":" + TiempoDeUso.Seconds;
                            ContadorPartidas++;

                        }
                        else
                        {
                            Console.WriteLine("el nombre no puede ser computadora");
                        }
                        Console.ReadKey();
                            break;
                    case 2:
                        Console.WriteLine("ingrese el nombre del jugador 1 (no puede ser computadora)");
                        jugador1 = Console.ReadLine();
                        Console.WriteLine("ingrese el nombre del jugador 2 (no puede ser computadora)");
                        jugador2 = Console.ReadLine();
                        jugadorActual = "";
                        tablero = new string[filas, columnas];
                        limpiarTablero(tablero);
                        FichaActual = elegirJugadorAlAzar();
                        if (FichaActual == Ficha1)
                        {
                            jugadorActual = jugador1;
                        }
                        else
                        {
                            if (FichaActual == Ficha2)
                            {
                                jugadorActual = jugador2;
                            }
                        }
                        if (jugador1 != "computadora" && jugador2 != "computadora")
                        {
                            TurnosJugador1 = 0;
                            TurnosJugador2 = 0;
                            tiempo.Restart();
                            tiempo.Start();


                            Console.WriteLine("Comienza el jugador " + jugadorActual);
                            while (true)
                            {
                                int columna = 0;
                                Console.WriteLine("\nTurno del jugador " + jugadorActual);
                                dibujarTablero(tablero);


                                columna = solicitarColumnaAJugador();
                                if (jugadorActual == jugador1)
                                {
                                    TurnosJugador1++;
                                }
                                else
                                {
                                    if (jugadorActual == jugador2)
                                    {
                                        TurnosJugador2++;
                                    }
                                }

                                int estado = colocarPieza(FichaActual, columna, tablero);
                                if (estado == ErrorColumnaLlena)
                                {
                                    Console.Write("Error: columna llena");
                                }
                                else if (estado == ErrorFilaInvalida)
                                {
                                    Console.Write("Fila no correcta");
                                }
                                else if (estado == ErrorNinguno)
                                {
                                    int g = ganador(FichaActual, tablero);
                                    if (g != NoConecta)
                                    {
                                        dibujarTablero(tablero);
                                        int turnoJ = 0;
                                        if (jugadorActual == jugador1)
                                        {
                                            turnoJ = TurnosJugador1;
                                        }
                                        else
                                        {
                                            if (jugadorActual == jugador2)
                                            {
                                                turnoJ = TurnosJugador2;
                                            }
                                        }
                                        Console.Write("Gana el jugador " + jugadorActual + " en " + turnoJ + " turnos ");
                                        if (ganador(FichaActual, tablero) == ConectaArriba)
                                        {
                                            Console.WriteLine("conectando en vertical");
                                        }
                                        else
                                        {
                                            if (ganador(FichaActual, tablero) == ConectaDerecha)
                                            {
                                                Console.WriteLine("conectando en horizontal");
                                            }
                                            else
                                            {
                                                if (ganador(FichaActual, tablero) == ConectaAbajoDerecha || ganador(FichaActual, tablero) == ConectaArribaDerecha)
                                                {
                                                    Console.WriteLine("conectando en diagonal");
                                                }
                                            }
                                        }
                                        break;
                                    }
                                    else if (esEmpate(tablero))
                                    {
                                        dibujarTablero(tablero);
                                        System.Console.Write("Empate");
                                        break;
                                    }
                                }
                                if (estado != ErrorColumnaLlena && estado != ErrorFilaInvalida)
                                {

                                        FichaActual = obtenerOponente(FichaActual);
                                    if (FichaActual == Ficha1)
                                    {
                                        jugadorActual = jugador1;
                                    }
                                    else
                                    {
                                        if (FichaActual == Ficha2)
                                        {
                                            jugadorActual = jugador2;
                                        }
                                    }
                                }

                            }
                            tiempo.Stop();
                            TiempoDeUso = tiempo.Elapsed;
                            ganadores[ContadorPartidas] = "Gana el jugador " + jugadorActual + " en " + TurnosJugador1 + " turnos, tiempo: " + TiempoDeUso.Minutes + ":" + TiempoDeUso.Seconds;
                            ContadorPartidas++;
                        }
                        else
                        {
                            Console.WriteLine("EL nombre de ninguno de los jugadores puede ser computadora");
                        }
                        Console.ReadKey();
                            break;
                    case 3:
                        for (int i = 0; i < 10;)
                        {
                            Console.WriteLine(ganadores[i]);
                            i++;
                        }
                        Console.ReadKey();
                        break;
                    case 4:
                        rep = 1;
                        break;
                    default:
                        Console.WriteLine("la opcion escogida es invalida");
                        Console.ReadKey();
                        break;
                }
            } while (rep == 0);
        }


        static void limpiarTablero(string[,] tablero)
        {
            int i;
            for (i = 0; i < filas; ++i)
            {
                int j;
                for (j = 0; j < columnas; ++j)
                {
                    tablero[i, j] = EspacioVacio;
                }
            }
        }

        static string elegirJugadorAlAzar()
        {
            int numero = aleatorio_en_rango(0, 1);
            if (numero == 1)
            {
                return Ficha1;
            }
            else
            {
                return Ficha2;
            }
        }

        static void dibujarEncabezado(int columnas)
        {
            Console.Write("\n");
            int i;
            for (i = 0; i < columnas; ++i)
            {
                Console.Write("|" + (i + 1));
                if (i + 1 >= columnas)
                {
                    Console.Write("|");
                }

            }
        }

        static int dibujarTablero(string[,] tablero)
        {
            dibujarEncabezado(columnas);
            Console.Write("\n");
            int i;
            for (i = 0; i < filas; ++i)
            {
                int j;
                for (j = 0; j < columnas; ++j)
                {
                    Console.Write("|" + tablero[i, j]);
                    if (j + 1 >= columnas)
                    {
                        Console.Write("|");
                    }
                }
                Console.Write("\n");
            }
            return 0;
        }

        static int solicitarColumnaAJugador()
        {
            Console.Write("Escribe la columna en donde colocar la pieza: ");
            int columna = Convert.ToInt32(Console.ReadLine());
            columna--;
            return columna;
        }

        static int colocarPieza(string jugador, int columna, string[,] tablero)
        {
            if (columna < 0 || columna >= columnas)
            {
                return ErrorFilaInvalida;
            }
            int fila = obtenerFilaDesocupada(columna, tablero);
            if (fila == FilaNoEncontrada)
            {
                return ErrorColumnaLlena;
            }
            tablero[fila, columna] = jugador;
            return ErrorNinguno;
        }


        static int ganador(string jugador, string[,] tablero)
        {
            int y;
            for (y = 0; y < filas; y++)
            {
                int x;
                for (x = 0; x < columnas; x++)
                {
                    int conteoArriba = contarArriba(x, y, jugador, tablero);
                    if (conteoArriba >= conecta)
                    {
                        return ConectaArriba;
                    }
                    if (contarDerecha(x, y, jugador, tablero) >= conecta)
                    {
                        return ConectaDerecha;
                    }
                    if (contarArribaDerecha(x, y, jugador, tablero) >= conecta)
                    {
                        return ConectaArribaDerecha;
                    }
                    if (contarAbajoDerecha(x, y, jugador, tablero) >= conecta)
                    {
                        return ConectaAbajoDerecha;
                    }
                }
            }
            return NoConecta;
        }

        static int contarArriba(int x, int y, string jugador, string[,] tablero)
        {
            int yInicio = (y - conecta >= 0) ? y - conecta + 1 : 0;
            int contador = 0;
            for (; yInicio <= y; yInicio++)
            {
                if (tablero[yInicio, x] == jugador)
                {
                    contador++;
                }
                else
                {
                    contador = 0;
                }
            }
            return contador;
        }

        static int contarDerecha(int x, int y, string jugador, string[,] tablero)
        {
            int xFin = (x + conecta < columnas) ? x + conecta - 1 : columnas - 1;
            int contador = 0;
            for (; x <= xFin; x++)
            {
                if (tablero[y, x] == jugador)
                {
                    contador++;
                }
                else
                {
                    contador = 0;
                }
            }
            return contador;
        }

        static int contarArribaDerecha(int x, int y, string jugador, string[,] tablero)
        {
            int xFin = (x + conecta < columnas) ? x + conecta - 1 : columnas - 1;
            int yInicio = (y - conecta >= 0) ? y - conecta + 1 : 0;
            int contador = 0;
            while (x <= xFin && yInicio <= y)
            {
                if (tablero[y, x] == jugador)
                {
                    contador++;
                }
                else
                {
                    contador = 0;
                }
                x++;
                y--;
            }
            return contador;
        }

        static int contarAbajoDerecha(int x, int y, string jugador, string[,] tablero)
        {
            int xFin = (x + conecta < columnas) ? x + conecta - 1 : columnas - 1;
            int yFin = (y + conecta < filas) ? y + conecta - 1 : filas - 1;
            int contador = 0;
            while (x <= xFin && y <= yFin)
            {
                if (tablero[y, x] == jugador)
                {
                    contador++;
                }
                else
                {
                    contador = 0;
                }
                x++;
                y++;
            }
            return contador;
        }

        static bool esEmpate(string[,] tablero)
        {
            int i;
            for (i = 0; i < columnas; ++i)
            {
                int resultado = obtenerFilaDesocupada(i, tablero);
                if (resultado != FilaNoEncontrada)
                {
                    return false;
                }
            }
            return true;
        }

        static string obtenerOponente(string jugador)
        {
            if (jugador == Ficha1)
            {
                return Ficha2;
            }
            else
            {
                return Ficha1;
            }
        }

        static int aleatorio_en_rango(int minimo, int maximo)
        {
            System.Random rnd = new System.Random();
            return rnd.Next(minimo, maximo + 1);
        }

        static int obtenerFilaDesocupada(int columna, string[,] tablero)
        {
            int i;
            for (i = filas - 1; i >= 0; i--)
            {
                if (tablero[i, columna] == EspacioVacio)
                {
                    return i;
                }
            }
            return FilaNoEncontrada;
        }


        static int cpu()
        {
            Random rnd = new Random();
            int columnacpu = rnd.Next(0,6);
            return columnacpu;
        }

        



    }
}
