# Feedback assignment 06.1

Obecně to víceméně funguje, mám několik připomínek

1. Update a Delete nefungují - každé z jiného důvodu, mělo by být jasné z komentářů proč, kdyby ne, tak napiš a dořešíme to :)
2. Ideální je si nejenom otestovat že kód jde zbuildit (tím zjistíme kompilačný errory), ale taky si kód zpustit a otestovat jak se chová - tím zjistíme logické errory které tady například nastaly = kód je v pořádku, ale nedělá to co má
3. zvláštnost s Delete metodou v ToDoItemsRepository, to se teď asi trochu více rozpovídám :)

Třída ToDoItemRepository implementuje interface IRepository. Interface můžeme chápat jako komunikační rozhraní - definuje nám co třída umí za metody, properties, atd... Tím vlastně bez ohledu na implementaci víme že třída umí metody X,Y a jaké tyto metody mají návratové hodnoty. (Třída může implemetovat vícero interfaces plus umět vlastní metody a mít vlastní properties, ale o to nám momentálně nejde).
Pokud se podíváš do našeho kontroléru, tak uvidíš že nepracujeme s ToDoItemsRepository, nýbrž s IRepository.

Pokud se podíváš do svého IRepository, tak ta deklaruje metodu
```
public void Delete(T itemToDelete);
```
T budeme chápat jako ToDoItem.Tuto metodu pak voláme z kontroléru protože v kontroléru pracujeme s IRepository, ne s ToDoItemsRepository

Tvůj ToDoItemRepository (které implementuje interface IRepository) má tyto dvě metody

```
public void Delete(int toDoItemId)
    {
        var itemToDelete = context.ToDoItems.Find(toDoItemId);

        if (itemToDelete != null)
        {
            context.ToDoItems.Remove(itemToDelete);
            context.SaveChanges();
        }
    }

public void Delete(ToDoItem itemToDelete) => throw new NotImplementedException();

```

Tak a pokud v ToDoItemRepository není
```
public void Delete(ToDoItem itemToDelete) => throw new NotImplementedException();
```
tak kompilér křčí. A křičí protože interface IRepository říká že máme metodu
```
 public void Delete(ToDoItem itemToDelete)
```
a ta naše třída ToDoItemsRepository nemá třítu public void Delete(ToDoItem itemToDelete)

Z toho důvodu nám je taky k ničemu metoda 
```
public void Delete(int toDoItemId)
    {
        var itemToDelete = context.ToDoItems.Find(toDoItemId);

        if (itemToDelete != null)
        {
            context.ToDoItems.Remove(itemToDelete);
            context.SaveChanges();
        }
    }
```
jelikož interface IRepository žádnou takovou metodu nezná.

No a když voláme Delete z kontoléru, tak voláme Delete(ToDoItem itemToDelete), čili naše HTTP Delete metoda vždy končí InternalServerError jelikož narazí na NotImplementedException()
