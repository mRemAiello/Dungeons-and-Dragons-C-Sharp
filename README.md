# Dungeons-and-Dragons

Piccolo progetto console in C# ispirato a Dungeons & Dragons. Il programma permette di creare un avventuriero, generare statistiche casuali, scegliere razza e classe, quindi avviare un ciclo di esplorazione con possibili incontri contro mostri e combattimenti a turni.

## Panoramica

Il progetto e' una console app .NET 8 composta da un unico eseguibile. La logica principale si trova in [D&D/Program.cs](e:/C#%20Projects/Dungeons-and-Dragons/D&D/Program.cs) e usa classi dedicate per:

- personaggi e mostri
- razze e modificatori alle statistiche
- classi dell'avventuriero
- oggetti equipaggiabili e drop
- dadi e generazione casuale

## Funzionalita' attualmente implementate

- Creazione del personaggio tramite input da console.
- Scelta della razza tra Umano, Nano, Elfo e Gnomo.
- Scelta della classe tra Barbaro, Mago, Paladino e Ladro.
- Generazione casuale delle caratteristiche principali.
- Gestione di salute, livello, denaro, inventario ed equipaggiamento.
- Combattimento base con attacco ravvicinato e attacco a distanza.
- Generazione casuale di nemici con possibili drop alla morte.

## Flusso di gioco

All'avvio il gioco chiede:

1. nome del personaggio
2. razza
3. classe

Dopo la creazione, il personaggio entra in un ciclo continuo finche' rimane in vita:

- con il 60% di probabilita' viene mostrato un messaggio di esplorazione
- con il 20% di probabilita' avviene un incontro con un nemico
- nel restante 20% il ciclo prosegue senza un evento visibile

Durante il combattimento il giocatore puo' scegliere se usare:

- attacco ravvicinato
- attacco a distanza

Il nemico risponde scegliendo casualmente tra le stesse due opzioni.

## Struttura del progetto

- [D&D/Program.cs](e:/C#%20Projects/Dungeons-and-Dragons/D&D/Program.cs): entry point e ciclo principale del gioco.
- [D&D/Classes/Adventurer.cs](e:/C#%20Projects/Dungeons-and-Dragons/D&D/Classes/Adventurer.cs): modello base del personaggio, statistiche, inventario, equipaggiamento, attacchi e controlli.
- [D&D/Classes/Monster.cs](e:/C#%20Projects/Dungeons-and-Dragons/D&D/Classes/Monster.cs): estensione di Adventurer con sistema di drop alla morte.
- [D&D/Classes](e:/C#%20Projects/Dungeons-and-Dragons/D&D/Classes): classi giocabili e nemici.
- [D&D/Races](e:/C#%20Projects/Dungeons-and-Dragons/D&D/Races): razze e modificatori alle caratteristiche.
- [D&D/Items](e:/C#%20Projects/Dungeons-and-Dragons/D&D/Items): oggetti, slot, armature, armi, scudi e drop.
- [D&D/Core](e:/C#%20Projects/Dungeons-and-Dragons/D&D/Core): gestione dei dadi e valori disponibili.

## Statistiche del personaggio

Le caratteristiche generate sono:

- Strength
- Dexterity
- Constitution
- Intelligence
- Wisdom
- Charisma

Ogni statistica viene tirata con un d20 e poi portata a un minimo di 8, prima di applicare i modificatori razziali.

La salute iniziale e' calcolata come:

$$
2 + tiro\_vita\_classe + Constitution
$$

## Razze disponibili

I modificatori attualmente presenti nel codice sono:

| Razza | STR | DEX | COS | INT | SAG | CAR |
| --- | ---: | ---: | ---: | ---: | ---: | ---: |
| Umano | -2 | +2 | 0 | +2 | 0 | 0 |
| Nano | 0 | 0 | +2 | 0 | +2 | -2 |
| Elfo | -2 | +2 | 0 | +2 | 0 | 0 |
| Gnomo | -2 | 0 | +2 | 0 | 0 | +2 |

## Classi disponibili

| Classe | Tiro salute | Denaro iniziale |
| --- | --- | --- |
| Barbaro | 1d12 | 3d12 |
| Mago | 1d6 | 3d6 |
| Paladino | 1d10 | 5d10 |
| Ladro | 1d6 | 4d6 |

## Sistema di combattimento

- L'attacco ravvicinato usa livello + Strength sul tiro per colpire.
- L'attacco a distanza usa livello + Dexterity sul tiro per colpire.
- La classe armatura base parte da 10 + Dexterity / 2.
- Alcuni oggetti equipaggiati possono aumentare la difesa o il danno.

Se un attacco supera la classe armatura del bersaglio, viene tirato il danno dell'arma equipaggiata e sottratto ai punti vita correnti.

## Nemici attualmente presenti

Il codice genera due tipi di nemico in modo casuale:

- Elfo con classe Barbarian, armato con una spada magica e con drop garantito di una spada.
- Nemico chiamato "Orco" costruito con razza Gnome e classe Paladin, equipaggiato con un'armatura di cuoio e con drop garantito di un'ascia.

## Requisiti

- .NET SDK 8.0 o superiore
- Windows, macOS o Linux con terminale compatibile con .NET

## Avvio del progetto

Dalla root della repository:

```bash
dotnet build D&D.sln
dotnet run --project D&D/D&D.csproj
```

## Stato attuale e limitazioni note

Il progetto compila correttamente, ma dal codice emergono alcune limitazioni attuali:

- Il personaggio del giocatore viene creato senza equipaggiamento iniziale, anche se esiste un metodo dedicato per assegnarlo
- Non è presente un menu di gestione dell'inventario o dell'equipaggiamento durante la partita
- Il ciclo di gioco non prevede vittoria, salvataggio o uscita guidata
- Il sistema di progressione esiste solo in forma base e non è integrato nel flusso principale
- Alcune parti sembrano ancora in evoluzione o non ancora collegate al gameplay completo

## Possibili sviluppi

- Assegnare l'equipaggiamento iniziale al personaggio appena creato
- Aggiungere menu per inventario, equip, loot e negozio
- Introdurre esperienza, level up e ricompense nel loop principale
- Ampliare il bestiario, le armi e gli eventi di esplorazione
- Migliorare il bilanciamento delle statistiche e delle regole di combattimento