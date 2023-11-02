using System;
using System.IO;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace DeckDiff.Tests;

public class UnitTest1
{
    private readonly string _oldDeck =
        """
        //Main
        1 Anikthea, Hand of Erebos #!Commander
        1 Aegis of the Gods
        1 Alseid of Life's Bounty
        1 Arcane Signet
        1 Archetype of Courage
        1 Archetype of Finality
        1 Aura Shards
        1 Aura of Silence
        1 Battle for Bretagard
        1 Beast Within
        1 Bow of Nylea
        1 Cacophony Unleashed
        1 Canopy Vista
        1 Cemetery Tampering
        1 Collective Blessing
        1 Command Tower
        1 Courser of Kruphix
        1 Cultivate
        1 Destiny Spinner
        1 Doomwake Giant
        1 Eidolon of Blossoms
        1 Elemental Bond
        1 Enchantress's Presence
        1 Endless Ranks of the Dead
        1 Evolving Wilds
        1 Exotic Orchard
        1 Extinguish All Hope
        1 Farseek
        1 Fertile Ground
        1 Font of Fertility
        14 Forest
        1 Ghostly Prison
        1 Ghoulish Impetus
        1 Gravebreaker Lamia
        1 Grim Guardian
        1 Grisly Salvage
        1 Growing Ranks
        1 Herald of the Pantheon
        1 Heroic Intervention
        1 Jukai Naturalist
        1 Khalni Heart Expedition
        1 Kodama's Reach
        1 Lightning Greaves
        1 Mesa Enchantress
        1 Mirari's Wake
        1 Nature's Lore
        1 Nyx Weaver
        1 Nyxborn Behemoth
        1 Omen of the Hunt
        1 Ondu Spiritdancer
        8 Plains
        1 Privileged Position
        1 Reliquary Tower
        1 Sanctum Weaver
        1 Sandsteppe Citadel
        1 Satyr Enchanter
        1 Seal of Cleansing
        1 Seal of Primordium
        1 Second Harvest
        1 Setessan Champion
        1 Skull Prophet
        1 Sol Ring
        1 Song of the Worldsoul
        1 Starfield Mystic
        1 Sterling Grove
        5 Swamp
        1 Swords to Plowshares
        1 Sythis, Harvest's Hand
        1 The Eldest Reborn
        1 Trostani, Selesnya's Voice
        1 True Conviction
        1 Underworld Coinsmith
        1 Vault of the Archangel
        1 Verduran Enchantress
        1 Weaver of Harmony
        1 Wild Growth
        """;


    private readonly string _newDeck =
        """
        //Main
        1 Anikthea, Hand of Erebos #!Commander
        1 Arcane Signet
        1 Archetype of Endurance
        1 Archetype of Finality
        1 Aura Shards
        1 Boon of the Spirit Realm
        1 Bow of Nylea
        1 Cacophony Unleashed
        1 Cemetery Tampering
        1 Circle of the Land Druid
        1 Codex Shredder
        1 Collective Blessing
        1 Command Tower
        1 Commune with the Gods
        1 Crawling Infestation
        1 Crucible of Worlds
        1 Deadbridge Chant
        1 Debtors' Knell
        1 Doomwake Giant
        1 Dryad of the Ilysian Grove
        1 Endless Ranks of the Dead
        1 Exotic Orchard
        1 Fall of the Thran
        9 Forest
        1 Geier Reach Sanitarium
        1 Golgari Signet
        1 Gravebreaker Lamia
        1 Great Hall of the Citadel
        1 Grim Guardian
        1 Grisly Salvage
        1 Growing Ranks
        1 In the Trenches
        1 Legion Loyalty
        1 Liliana's Mastery
        1 Mesmeric Orb
        1 Millikin
        1 Mindwrack Harpy
        1 Mirari's Wake
        1 Monster Manual // Zoological Study
        1 Mulch
        1 Necromancer's Covenant
        1 Necromancer's Stockpile
        1 Null Profusion
        1 Nyx Weaver
        1 Omen of the Hunt
        1 Orzhov Signet
        1 Out of the Tombs
        1 Perpetual Timepiece
        5 Plains
        1 Privileged Position
        1 Raised by Giants
        1 Rampage of the Valkyries
        1 Recycle
        1 Sanctum Weaver
        1 Sandsteppe Citadel
        1 Satyr Wayfinder
        1 Seal from Existence
        1 Second Harvest
        1 Selesnya Signet
        1 Shigeki, Jukai Visionary
        1 Shriekhorn
        1 Skull Prophet
        1 Sol Ring
        1 Sphere of Safety
        1 Sterling Grove
        1 Stitcher's Supplier
        1 Strionic Resonator
        9 Swamp
        1 Temple of Malady
        1 Temple of Plenty
        1 Temple of Silence
        1 The Eldest Reborn
        1 Thoughtrender Lamia
        1 True Conviction
        1 Undead Butler
        1 Underworld Coinsmith
        1 Vault of the Archangel
        1 Vessel of Nascency
        1 Weaver of Harmony
        1 Zombie Infestation
        """;


    private readonly string _toRemoveExpected =
        """
        1 Aegis of the Gods
        1 Alseid of Life's Bounty
        1 Archetype of Courage
        1 Aura of Silence
        1 Battle for Bretagard
        1 Beast Within
        1 Canopy Vista
        1 Courser of Kruphix
        1 Cultivate
        1 Destiny Spinner
        1 Eidolon of Blossoms
        1 Elemental Bond
        1 Enchantress's Presence
        1 Evolving Wilds
        1 Extinguish All Hope
        1 Farseek
        1 Fertile Ground
        1 Font of Fertility
        5 Forest
        1 Ghostly Prison
        1 Ghoulish Impetus
        1 Herald of the Pantheon
        1 Heroic Intervention
        1 Jukai Naturalist
        1 Khalni Heart Expedition
        1 Kodama's Reach
        1 Lightning Greaves
        1 Mesa Enchantress
        1 Nature's Lore
        1 Nyxborn Behemoth
        1 Ondu Spiritdancer
        3 Plains
        1 Reliquary Tower
        1 Satyr Enchanter
        1 Seal of Cleansing
        1 Seal of Primordium
        1 Setessan Champion
        1 Song of the Worldsoul
        1 Starfield Mystic
        1 Swords to Plowshares
        1 Sythis, Harvest's Hand
        1 Trostani, Selesnya's Voice
        1 Verduran Enchantress
        1 Wild Growth
        """;

    private readonly string _toAddExpected =
        """
        1 Archetype of Endurance
        1 Boon of the Spirit Realm
        1 Circle of the Land Druid
        1 Codex Shredder
        1 Commune with the Gods
        1 Crawling Infestation
        1 Crucible of Worlds
        1 Deadbridge Chant
        1 Debtors' Knell
        1 Dryad of the Ilysian Grove
        1 Fall of the Thran
        1 Geier Reach Sanitarium
        1 Golgari Signet
        1 Great Hall of the Citadel
        1 In the Trenches
        1 Legion Loyalty
        1 Liliana's Mastery
        1 Mesmeric Orb
        1 Millikin
        1 Mindwrack Harpy
        1 Monster Manual // Zoological Study
        1 Mulch
        1 Necromancer's Covenant
        1 Necromancer's Stockpile
        1 Null Profusion
        1 Orzhov Signet
        1 Out of the Tombs
        1 Perpetual Timepiece
        1 Raised by Giants
        1 Rampage of the Valkyries
        1 Recycle
        1 Satyr Wayfinder
        1 Seal from Existence
        1 Selesnya Signet
        1 Shigeki, Jukai Visionary
        1 Shriekhorn
        1 Sphere of Safety
        1 Stitcher's Supplier
        1 Strionic Resonator
        4 Swamp
        1 Temple of Malady
        1 Temple of Plenty
        1 Temple of Silence
        1 Thoughtrender Lamia
        1 Undead Butler
        1 Vessel of Nascency
        1 Zombie Infestation
        """;

    [Fact]
    public void Test1()
    {

        var result = Deck.Compare(_oldDeck, _newDeck);
        Assert.Equal(_toRemoveExpected, result.toRemove);
        Assert.Equal(_toAddExpected, result.toAdd);
    }

    [Fact]
    public async void CompareFromFile()
    {
        string[] args =
            {
                "--old-deck-file", @"..\..\..\resources\input\Anikthea.txt",
                "--new-deck-file", @"..\..\..\resources\input\Anikthea-2.txt",
                "--output-dir", @"..\..\..\output"
            };
        await Program.Main(args);
        var toRemoveResult = File.ReadAllText(@"..\..\..\output\Anikthea-remove.txt");
        var toAddResult = File.ReadAllText(@"..\..\..\output\Anikthea-2-add.txt");
        Assert.Equal(_toRemoveExpected, toRemoveResult);
        Assert.Equal(_toAddExpected, toAddResult);
    }
}