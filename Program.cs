using System;
using System.IO;
using System.Collections;
using AutomatizarPruebasUnitarias;
using System.Collections.Generic;

namespace automatizar_pruebas_unitarias_2019_edgar8acas
{
    class Program
    {
        
        static void Main(string[] args)
        {
            List<String> testCases = new List<String>();
            try
            {
                using(StreamReader sr = new StreamReader("CasosPrueba.txt")) 
                {
                    string line;
                    while((line = sr.ReadLine()) != null)
                    {
                        testCases.Add(line);
                    }
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Error al leer el archivo: " + e.Message);
            }

            string resultsString = "ID" + "\t\t" + "RESULTADO" + "\t\t" + "MÉTODO" + "\t\t" + "TIEMPO (ms)" + "\t\t" + "CALCULADO" + "\t\t" + "ESPERADO";
            Console.WriteLine(resultsString);
            resultsString += "\n\r";
            int exitos = 0;
            int fallas = 0;
            foreach (var testCase in testCases)
            {
                String[] splittedData = splitCase(testCase);
                string[] stringInputs = splittedData[2].Split(" ");
                object[] inputs = new object[stringInputs.Length];
                for (int i = 0; i < stringInputs.Length; i++)
                {
                    inputs[i] = convertData(stringInputs[i]);
                }
                object expected = convertData(splittedData[3]);
                object testResult = null;
                double timeTaken = 0;
                string assertionResult;

                if(assert(expected, inputs, splittedData[1], ref testResult, ref timeTaken)){
                    assertionResult = "Éxito";
                    exitos++;
                } else {
                    assertionResult = "Falla";
                    fallas++;
                }

                if (expected.GetType().Name == "Double"){
                    expected = (double) expected;
                } else {
                    expected = (string) expected;
                }

                string resultsCaseStringPart1 = splittedData[0] + "\t\t";  
                string resultsCaseStringPart2 = "\t\t" + splittedData[1] + "\t\t" + timeTaken + "\t\t" + testResult + "\t\t" + expected + "\n\r";
                resultsString += resultsCaseStringPart1 + assertionResult + resultsCaseStringPart2; 
                
                Console.Write(resultsCaseStringPart1);
                colorOutput(assertionResult);
                Console.Write(resultsCaseStringPart2);
            }
            string finalString = $"FIN DE LA PRUEBA \r\nÉxitos: { exitos } \r\nFallas: { fallas } \n\r";
            resultsString += finalString;

            using (StreamWriter sw = new StreamWriter("ResultadosPrueba.txt"))
            {
                sw.Write(resultsString);
            }
            Console.Write(finalString);
        }
        private static String[] splitCase(String testCase) 
        {
            return testCase.Split(":");
        }

        private static object convertData(String data)
        {
            object convertedData;
            switch (data)
            {
                case "NULL": 
                    convertedData = null;
                break;

                case "Exception":
                    convertedData = data;
                break;

                default:
                    convertedData = Convert.ToDouble(data);
                break;
            }
            return convertedData;
        }
        private static object test(object[] inputs, String methodToTest)
        {
            switch (methodToTest)
            {
                case "mediaAritmetica":
                    return Medias.mediaAritmetica(inputs);

                case "mediaGeometrica":
                    return Medias.mediaGeometrica(inputs);

                case "mediaArmonica":
                    return Medias.mediaArmonica(inputs);
                
                default:
                    throw new Exception("Unvalid method");
                
            }
            
        }

        private static bool assert(object expected, object[] inputs, String methodToTest, ref object result, ref double timeTaken)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                result = (double) test(inputs, methodToTest);

                watch.Stop();
                timeTaken = watch.Elapsed.Ticks / 10000d;

                if ((double) expected == (double) result)
                    return true;
                return false;
            }
            catch (System.Exception)
            {
                watch.Stop();
                timeTaken = watch.Elapsed.Ticks / 10000d;

                if((string) expected == "Exception")
                {
                    result = "Exception";
                    return true;     
                }
                return false;
            }
        }

        private static void colorOutput(string assertionResult) 
        {
            if(assertionResult == "Éxito")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(assertionResult);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(assertionResult);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
