public class Adventurer
{
    private string? _name;
    private int _strength;
    private int _dexterity;
    private int _constitution;
    private int _intelligence;
    private int _wisdom;
    private int _charisma;
    private int _level;
    private int _money;
    private int _experience;
    private int _maxHealth;
    private int _currentHealth;
    private List<Item> _inventory = new List<Item>();
    private Dictionary<ItemSlot, IEquipable?> _equippedItems = new Dictionary<ItemSlot, IEquipable?>();

    public int GetStrength() => _strength;
    public int GetDexterity() => _dexterity;
    public int GetConstitution() => _constitution;
    public int GetIntelligence() => _intelligence;
    public int GetWisdom() => _wisdom;
    public int GetCharisma() => _charisma;
    public int GetLevel() => _level;
    public int GetMoney() => _money;
    public string? Name => _name;
    public bool IsAlive => _currentHealth > 0;

    public Adventurer(string? name, Race race, AdventurerClass adventurerClass)
    {
        _name = name;
        _level = 1;
        _experience = 0;
        _strength = NotLowerThan8(Dice.RollD20()) + race.StrengthModifier;
        _dexterity = NotLowerThan8(Dice.RollD20()) + race.DexterityModifier;
        _constitution = NotLowerThan8(Dice.RollD20()) + race.ConstitutionModifier;
        _intelligence = NotLowerThan8(Dice.RollD20()) + race.IntelligenceModifier;
        _wisdom = NotLowerThan8(Dice.RollD20()) + race.WisdomModifier;
        _charisma = NotLowerThan8(Dice.RollD20()) + race.CharismaModifier;
        _maxHealth = 2 + adventurerClass.HealthRoll() + _constitution;
        _currentHealth = _maxHealth;
        _money = adventurerClass.MoneyRoll();
        _equippedItems = new()
        {
            { ItemSlot.Weapon1, null },
            { ItemSlot.Weapon2, null },
            { ItemSlot.Shield, null },
            { ItemSlot.Head, null },
            { ItemSlot.Chest, null },
            { ItemSlot.Ring, null }
        };
        
    }
    private int NotLowerThan8(int value)
    {
        if (value < 8)
        {
            return 8;
        }
        return value;
    }

    public override string ToString()
    {
        string toString = $"Adventurer: {_name},\n";
        toString += $"Health: {_currentHealth}/{_maxHealth}\n";
        toString += $"Strength: {_strength}, ";
        toString += $"Dexterity: {_dexterity}, ";
        toString += $"Constitution: {_constitution}, ";
        toString += $"Intelligence: {_intelligence}, ";
        toString += $"Wisdom: {_wisdom}, ";
        toString += $"Charisma: {_charisma}.\n";
        toString += $"Money: {_money}\n";
        toString += $"Level: {_level}\n";
        toString += $"Helmet: {GetNameByItemSlot(ItemSlot.Head)}\n";
        toString += $"Chest: {GetNameByItemSlot(ItemSlot.Chest)}\n";
        toString += $"Ring: {GetNameByItemSlot(ItemSlot.Ring)}\n";
        toString += $"Weapon 1: {GetNameByItemSlot(ItemSlot.Weapon1)}\n";
        toString += $"Weapon 2: {GetNameByItemSlot(ItemSlot.Weapon2)}\n";
        toString += $"Shield: {GetNameByItemSlot(ItemSlot.Shield)}\n";
        toString += $"Armor Class: {GetArmorClass()}\n";
        return toString;
    }

    private string? GetNameByItemSlot(ItemSlot slot)
    {
        if (_equippedItems[slot] != null)
        {
            return _equippedItems[slot]?.Name;
        }
        return "Nessun oggetto equipaggiato.";
    }
    public int ExpNeeded(int level)
    {
        int expNeeded = (100 * level) * (level + 1) / 2;
        return expNeeded;
    }
    public void GainExperience(int exp)
    {
        _experience += exp;
        if (_experience >= ExpNeeded(_level))
        {
            _experience -= ExpNeeded(_level);
            LevelUp();
        }
    }

    public void LevelUp()
    {
        _level++;
    }

    public void GetMoney(int money)
    {
        _money += money;
    }

    public void AddItem(Item item)
    {
        _inventory.Add(item);
    }

    public void SellItem(Item item)
    {
        if (_inventory.Contains(item))
        {
            _money += item.Price;
            _inventory.Remove(item);
        }
    }

    public void BuyItem(Item item)
    {
        if (_money >= item.Price)
        {
            _money -= item.Price;
            _inventory.Add(item);
        }
        else
        {
            Console.WriteLine("Denaro insufficiente per acquistare l'oggetto.");
        }
    }

    public void Equip(IEquipable equipable)
    {
        if (equipable == null)
        {
            Console.WriteLine("[EquipItem]: L'oggetto non può essere null.");
            return;
        }
        if (_equippedItems[equipable.Slot] is Item item)
        {
            UnequipItem(equipable.Slot);
            _inventory.Add(item);
        }
        _equippedItems[equipable.Slot] = equipable;
    }

    public void UnequipItem(ItemSlot slot)
    {
        if (_equippedItems[slot] != null)
        {
            if (_equippedItems[slot] is Item item)
            {
                _inventory.Add(item);
                _equippedItems[slot] = null;
            }
            else
            {
                Console.WriteLine("Oggetto non valido.");
            }
        }
        else
        {
            Console.WriteLine("Nessun oggetto equipaggiato in questo slot.");
        }
    }

    public void ShowEquippedItems()
    {
        foreach (var item in _equippedItems)
        {
            if (item.Value != null)
            {
                Console.WriteLine($"{item.Key}: {item.Value.Name}");
            }
            else
            {
                Console.WriteLine($"{item.Key}: Nessun oggetto equipaggiato.");
            }
        }
    }

    public void ShowInventory()
    {
        Console.WriteLine("Inventario:");
        foreach (var item in _inventory)
        {
            Console.WriteLine(item.Name);
        }
    }

    public void AddToInventory(Item item)
    {
        if (item != null)
        {
            Console.WriteLine($"{_name} ha ottenuto {item.Name}.");
            _inventory.Add(item);
        }
        else
        {
            Console.WriteLine("L'oggetto non può essere null.");
        }
    }

    // Provare a fare funzione che restituisce IEquipable di uno slot, e se non è null restituire un IEquipable.
    // Fare il return dell'oggetto che viene dato e da quello prendere il valore di attacco o difesa.

    public IEquipable? GetEquippedItem(ItemSlot slot)
    {
        if (_equippedItems[slot] != null)
        {
            return _equippedItems[slot];
        }
        return null;
    }
    public int GetArmorClass()
    {
        int armorClass = 10 + (_dexterity / 2);
        if (_equippedItems[ItemSlot.Shield] is Shield shield)
        {
            armorClass += shield.Defense;
        }
        if (_equippedItems[ItemSlot.Head] is Armor armor)
        {
            armorClass += armor.Defense;
        }
        return armorClass;
    }

    public string GetWeaponInfo()
    {
        if (_equippedItems[ItemSlot.Weapon1] is Weapon weapon)
        {
            return $"Arma: {weapon.Name}, Danno: {weapon.Damage}";
        }
        return "Non hai equipaggiato un'arma nello slot 1.";
    }
    public string GetRangedWeaponInfo()
    {
        if (_equippedItems[ItemSlot.Weapon2] is RangedWeapon rangedWeapon)
        {
            return $"Arma a distanza: {rangedWeapon.Name}, Danno: {rangedWeapon.Damage}";
        }
        return "Non hai equipaggiato un'arma a distanza nello slot 2.";
    }
    public string GetShieldInfo()
    {
        if (_equippedItems[ItemSlot.Shield] is Shield shield)
        {
            return $"Scudo: {shield.Name}, Difesa: {shield.Defense}";
        }
        return "Non hai equipaggiato uno scudo.";
    }
    public string GetArmorInfo()
    {
        if (_equippedItems[ItemSlot.Chest] is Armor armor)
        {
            return $"Armatura: {armor.Name}, Difesa: {armor.Defense}";
        }
        return "Non hai equipaggiato un'armatura.";
    }

    


    // Fare funzioni per ottenere dati degli oggetti (GetShield ecc.)
    public int MeleeAttack(Adventurer target)
    {
        int attackRoll = Dice.RollD20() + _level + _strength;
        if (attackRoll > target.GetArmorClass())
        {
            int damage = 0;
            var equipped = GetEquippedItem(ItemSlot.Weapon1);
            if (equipped is Weapon weapon)
            {
                damage += weapon.GetDamageRoll();
                Console.WriteLine($"{_name} ha inflitto {damage} danni a {target.Name}.");
                target.TakeDamage(this, damage);
            }
            else
            {
                Console.WriteLine("Non hai equipaggiato un'arma.");
                return 0;
            }
            return damage;
        }
        Console.WriteLine("Attacco fallito!");
        return 0;
    }

    public int RangedAttack(Adventurer target)
    {
        int attackRoll = Dice.RollD20() + _level + _dexterity;
        if (attackRoll > target.GetArmorClass())
        {
            int damage = 0;
            var equipped = GetEquippedItem(ItemSlot.Weapon2);
            if (equipped is RangedWeapon rangedWeapon)
            {
                damage += rangedWeapon.GetDamageRoll();
                Console.WriteLine($"{_name} ha inflitto {damage} danni a {target.Name}.");
                target.TakeDamage(this, damage);
            }
            else
            {
                Console.WriteLine("Non hai equipaggiato un'arma a distanza.");
                return 0;
            }
            return damage;
        }
        Console.WriteLine("Attacco fallito!");
        return 0;
    }

    public int TakeDamage(Adventurer attacker, int damage)
    {
        if (_currentHealth <= 0)
        {
            Console.WriteLine($"{_name} è già morto.");
            return _currentHealth;
        }

        _currentHealth -= damage;
        Console.WriteLine($"{_name} ha subito {damage} danni.");
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die(attacker);
        }
        return _currentHealth;
    }

    protected virtual void Die(Adventurer attacker)
    {
        Console.WriteLine($"{_name} è stato ucciso da {attacker.Name}.");
    }

    public bool PerceptionCheck(int check)
    {
        int roll = Dice.RollD20() + 2 + _wisdom;
        if (roll >= check)
        {
            Console.WriteLine("Controllo di percezione riuscito!");
            return true;
        }
        else
        {
            Console.WriteLine("Controllo di percezione fallito.");
            return false;
        }
    }

    public bool ListeningCheck(int check)
    {
        int roll = Dice.RollD20() + 2 + _charisma;
        if (roll >= check)
        {
            Console.WriteLine("Controllo di ascolto riuscito!");
            return true;
        }
        else
        {
            Console.WriteLine("Controllo di ascolto fallito.");
            return false;
        }
    }

    // Da istanziare in Program.cs dei mostri e provare il combattimento.
    // Implementare un sistema che permetta a chi uccide il mostro di ottenere i suoi oggetti.

}