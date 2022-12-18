# Partie 2 : Products-WebApi
 
 + Cette partie est une application DotNet Core de type WebAPI qui permet gérer des produits appartenant à des catégories.
 
## La création du projet
  + Via la commande : dotnet new webapi -o Products-WebApi
  
  ![image](https://user-images.githubusercontent.com/52087288/206789980-82a9256b-2cdd-4456-beb7-c05ca79c92ca.png)

## La structure du projet

## La création des classes principales
  + Product
    ```java
    public class Product
     {
    public int ProductId { get; set; }
    public string Designation { get; set; }
    public double Price { get; set; }
    
    public int CategoryID { get; set; }
    
    }
    ```
  + Category
    ```java
      public class Category
    {
    public  int CategoryID { get;  set; }
    public  string Name { get;  set; }
    public virtual ICollection<Product> Products { get; set; }
    }
    ```
  + ProductRestController
    ```java
    
    ```
  + CategoryRestController
    ```java
    
    ```
  
  
## L'exécution du projet
  
