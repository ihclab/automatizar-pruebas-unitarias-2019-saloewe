
int contador = 0;
string linea;

System.IO.StreamReader archivo =   
    new System.IO.StreamReader(@"C:\Users\Michelita\Desktop\automatizar-pruebas-unitarias-2019-MaricruzCF\CasosPrueba.txt");  
while((linea = archivo.ReadLine()) != null)  
{  
    System.Console.WriteLine(linea);  
    contador++;  
}  
  
archivo.Close();  
System.Console.WriteLine("There were {0} lines.", contador);  

System.Console.ReadLine();  


//ejercicio 3
string Casoprueba ="0001:mediaAritmetica:2 4 8:4.6666";
string[] casoprueba= Casoprueba.split(":");

//solo imprime
for (int i = 0; i < casoprueba.length; i++) {
	System.out.println(casoprueba[i]);