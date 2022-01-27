using System;

namespace Skalierungs
{
    class Program
    {
        const double pi = 3.1415926;

        static double PartitialVolume(double a, double b, double w1, double dw)
        {
            double x1 = a * Math.Cos(w1);
            double y1 = b * Math.Sin(w1);

            double x2 = a * Math.Cos(w1+dw);
            double y2 = b * Math.Sin(w1+dw);

            // Dreiecksfläche
            double v = 0.5*(x2-x1)*(y2-y1);

            if(x1 > 0.0)
            {
                v += x1*(y2-y1);
            }

            return v;
        }

        /// ScaleEllipse
        /// double a: großer Durchmesser in cm
        /// double b: kleiner Durchmesser in cm
        /// double l: Länge Fass in cm
        static void ScaleEllipse(double a, double b, double l)
        {
            a/=20;
            b/=20;
            l/=10;
            double v = a*b*pi*l;
            Console.WriteLine("Volumen vom Fass = {0} l", v);

            v = 0.0;
            int n = 20;
            double[] pv = new double[n];
            double delta = pi/2.0/n;

            // Teilvolumen für Viertelellipse berechnen
            for (int i=0; i<n;i++)
            {
                double alpha = 3.0*pi/2.0 + i*delta;
                pv[i] = PartitialVolume(a,b,alpha,delta)*l*2;
            }

            v = 0.0;
            int k=0;

            double min = 0.4+2.0*b;
            Console.WriteLine("{0:F4} {1:F2}", min/10, v);
            for (int i=0; i<2*n;i++)
            {              
                double alpha = 3.0*pi/2.0 + i*delta;
                double h = b+b*Math.Sin(alpha+delta);

                if(i<n)
                {
                    v += pv[k++];
                }
                else
                {
                    v += pv[--k];
                }

                //Console.WriteLine("Winkel {0:F2} Höhe {1:F2} Volumen {2:F2}", alpha*180.0/pi, h, v);
                Console.WriteLine("{0:F4} {1:F2}", (min-h)/10.0, v);
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Volumen Fass mit elliptischem Querschnitt");
            ScaleEllipse(120, 95, 225);
        }
    }
}
