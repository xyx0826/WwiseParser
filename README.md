# WwiseParser

WwiseParser is a library for parsing Wwise 2016 SoundBank object formats. It can be used to deserialize Wwise objects and rebuild hierarchies.

The library project, **WwiseParserLib**, is the core library for parsing in-memory and on-disk SoundBanks.

The CLI app, **WwiseParser**, is a frontend of WWiseParserLib that dumps SoundBank files to JSON exports.
It can also be used to dump Wwise objects into separate JSON files in the "inspector mode".

## Quickstart

The base class for SoundBank types is `WwiseParserLib.Structures.SoundBanks.SoundBank`.
Currently there are two concrete implementations: `FileSoundBank` and `InMemorySoundBank`.

The `FileSoundBank` is designed for reading `.bnk` files. Initialize it with:

```cs
SoundBank fileSoundBank = new WwiseParserLib.Structures.SoundBanks.FileSoundBank("C:\\Path\\To\\Bank.bnk");
```

The `InMemorySoundBank` is designed for reading SoundBank data as byte arrays. Initialize it with:

```cs
byte[] soundBankData;
// Fill array with actual data...
SoundBank inMemSoundBank = new WwiseParserLib.Structures.SoundBanks.InMemorySoundBank(soundBankData);
```

From here, use the `SoundBank.GetSection(SoundBankSectionName name)` method to (attempt to) get a parsed section.
`SoundBankSectionName` is an enum containing all possible SoundBank section names.
Currently, only `BKHD`, `HIRC`, and `STMG` are supported.

The returned section will be of base type `SoundBankSection`.

## Hierarchies

Currently, **Master-Mixer** and **Actor-Mixer** hierachies are supported. To rebuild a hierarchy:

- Create a `SoundBank` object.
- Call `SoundBank.CreateMasterMixerHierarchy()` or `SoundBank.CreateActorMixerHierarchy()` to
  rebuild respective hierarchies.

The `WwiseParserLib.Structures.Hierarchies.MasterMixerHierarchy` contains two `AudioBus`es,
`MasterMixerHierarchy.MasterAudioBus` and `MasterMixerHierarchy.MasterSecondaryBus`.
Hierarchy traversal can be done through their `Children` property.

The `WwiseParserLib.Structures.Hierarchies.ActorMixerHierarchy` contains a list of top-level
"actor objects", `Actors`. "Top-level actors" contain actor objects without a parent ID
(which are truly top-level objects), and actors with nonexistant parent IDs.

The second case can happen when an `ActorMixerHierarchy` is manually created and provided an incomplete
list of actor objects so that the hierarchy cannot be fully rebuilt.

Another case may be that the unreachable parent object lives in another SoundBank,
but this has yet to be proved.
