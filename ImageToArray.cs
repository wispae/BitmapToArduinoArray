//Laat het systeem weten welke Libraries we nodig hebben voor het programma
using System;
using System.IO;
using System.Drawing;

namespace ParseImage
{
    class Program
    {
        static void Main(string[] args)
        {
            //maak een nieuwe instantie van de Color klasse om kleuren uit de afbeelding te kunnen halen
            Color kleur = new Color();

            //Laat de huidige directory van de file zien (debug)
            Console.WriteLine(Directory.GetCurrentDirectory() + "\\output.txt");

            //Maak een nieuwe instantie aan van de StreamWriter klasse
            //Deze maakt en schrijft naar een tekstbestand in de huidige directory van het programma
            StreamWriter sw = File.CreateText((Directory.GetCurrentDirectory()+"\\output.txt"));

            //Check eerst of de bitmap 'image.bmp' in de huidige folder staat
            if (!File.Exists("image.bmp"))
            {
                //Stop het programma als de afbeelding niet in de folder staat
                Console.WriteLine("image.bmp not found!    Terminating program...");

                //Wacht met afsluiten tot de gebruiker op enter drukt
                Console.ReadLine();

                //Sluit het programma af met exit code 1 (Catchall for general errors)
                Environment.Exit(1);
            }

            //Maak een instantie van de Bitmap klasse aan met afbeelding 'image.bmp'
            Bitmap afbeelding = new Bitmap("image.bmp");

            //Slaag de hoogte en breedte van de afbeelding op om later te gebruiken
            int height = afbeelding.Height;
            int width = afbeelding.Width;

            //laat de hoogte en breedte aan de gebruiker weten
            Console.WriteLine("Hoogte: {0}",height);
            Console.WriteLine("Breedte: {0}", width);

            //Schrijf eerste deel van tekstbestand (array declaration)
            //
            //de writes zijn uiteengehaald omdat er anders een error kwam om de een of andere reden
            sw.Write("byte image");
            sw.Write("[{0}]",height);
            sw.Write("[{0}]", width);
            sw.Write("[3] = {");

            //Scrijf de verschillende delen van het array naar het bestand
            for (int a = 0; a <= height-1; a++)
            {
                sw.Write("{");
                for (int b = 0; b <= width-1; b++)
                {
                    sw.Write("{");
                    
                    //vraag de kleuren van de pixel op en schrijf deze naar het bestand
                    kleur = afbeelding.GetPixel(b, a);
                    sw.Write("{0},", kleur.R);
                    sw.Write("{0},", kleur.G);
                    sw.Write("{0}", kleur.B);
                    if(b != width - 1)
                    {
                        sw.Write("}, ");
                    }
                    else
                    {
                        sw.Write("}");
                    }
                }
                if(a != height-1)
                {
                    sw.WriteLine("},");
                }
                else
                {
                    sw.Write("}");
                }

                //als de StreamWriter niet tijdig geflushed wordt zal de buffer volgeraken
                //en kunnen latere stukken tekst niet geschreven worden
                sw.Flush();
            }
            sw.WriteLine("};");

            //Sluit de StreamWriter klasse af
            sw.Close();
            Console.WriteLine("Program Done");
            Console.ReadLine();
        }
    }
}
