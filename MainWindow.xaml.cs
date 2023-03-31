﻿using System;
using System.IO;
using System.Windows;
using Antlr4.Runtime;
using miniChart.Logica;

namespace miniChart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    ///
    /// Esta interfaz fue creada por Julio Cesar Castro Y Josue Orozco
    /// Se utilizo el lenguaje de programacion C# y el framework de WPF
    /// Tome en cuenta que esta interfaz es solo una propuesta, puede ser modificada
    /// Las funciones aqui implementadas son solo para mostrar como se puede implementar
    /// Las mismas estan vacias dentro de ellas esta el nombre de la funcion y un comentario que indica que es lo que hace la funcion
    /// 
    /// 
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow() 
        {
            // El System.Diagnostics.Debug.WriteLine es para imprimir en la consola del debugger.
            System.Diagnostics.Debug.WriteLine("System Diagnostics Debug");
            
            InitializeComponent(); 
        }
        private void Add_Tab_Button_Click(object sender, EventArgs e)
        {
            // Aqui va la logica para agregar una nueva pestaña
            // Al agregar la nueva pestaña tome en consideracion
            // que se debe agregar un nuevo TabItem y un nuevo TextBox
            // dentro del TabControl y que dentro del textbox se agrega el texto del archivo.txt que se subio
        }
        
        private void closeButton_Click(object sender, EventArgs e)
        {
            // Eliminar la pestaña seleccionada TabItem
            // Elimina una pestaña del TabControl, tome en cuenta que al eliminar una pestaña 
            // tambien se debe eliminar el TextBox que esta dentro del TabControl
        }

        private void Pantalla_SelectionChanged(object sender, EventArgs e)
        {
            UpdateCursorPosition();
        }
        private void Pantalla_TextChanged(object sender, EventArgs e)
        {
            UpdateCursorPosition();
        }   
        
        private void UpdateCursorPosition()
        {
            int index = Pantalla.SelectionStart;
            int line = Pantalla.GetLineIndexFromCharacterIndex(index) + 1;
            int column = index - Pantalla.GetCharacterIndexFromLineIndex(Pantalla.GetLineIndexFromCharacterIndex(index)) + 1;
         
            // Actualiza el texto de un label o de otro TextBox con el número de línea y columna.
            Output.Content = $"Línea: {line} \nColumna: {column}";
        }
        public void Upload_File_Button_Click(object? sender, RoutedEventArgs e) 
        {
            // Aqui va la logica para subir un archivo
            // AL subir el archivo tome en consideracion
            // que se agrega el archivo al tabItem y al TextBox que ya esta creado en el MainWindow.xaml
            // El tabItem tiene por nombre "Principal" y el TextBox tiene por nombre "Pantalla"
        }
        private void Run_Button_Click(object? sender, RoutedEventArgs e) 
        {
            // Aqui va la logica para correr el codigo
            // AL correr el codigo tome en consideracion
            // que el resultado de la c se muestra en una nueva ventana llamada Consola
            // en el texbox SalidaConsola
            RunMiniChart(CharStreams.fromString(Pantalla.Text.ToLower()));
        }
        
        private void Exit_Button_Click(object? sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Exit Button Clicked");
            Application.Current.Shutdown();
        }
        
        private void RunMiniChart(ICharStream pCode)
        {
            //ICharStream inputStream = CharStreams.fromPath(@"C:\Users\Mariana Artavia Vene\Documents\I SEMESTRE 2023\Compiladores e Interpretes\ConsoleCompi\ConsoleCompi\test.txt");
            var lexer = new MiniCSharpScanner(pCode);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            MiniCSharpParser parser = new MiniCSharpParser(tokens);
            var errorListener = new MyErrorListener();

            // Asigna el ErrorListener personalizado al parser.
            parser.RemoveErrorListeners(); // Elimina el ErrorListener predeterminado.
            parser.AddErrorListener(errorListener);
            var context = parser.program();
                
            if (parser.NumberOfSyntaxErrors > 0)
            {
                System.Diagnostics.Debug.WriteLine("Compilación fallida: " + parser.NumberOfSyntaxErrors + " error(es) de sintaxis encontrados.");
                Resultado.Content = "Compilación fallida: " + parser.NumberOfSyntaxErrors +
                                    " error(es) de sintaxis encontrados.";
                foreach (string error in errorListener.SyntaxErrors)
                {
                    System.Diagnostics.Debug.WriteLine(error);
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Compilación éxitosa!");
                Resultado.Content = "Compilación éxitosa!";
            }
        }
    }
    
}
