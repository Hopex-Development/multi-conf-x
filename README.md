# Description

Implements reading and modification of configuration files such as INI, ENV.

# Adding to the project

#### .NET CLI
```CLI
> dotnet add package Hopex.MultiConfX --version 23.0.2
```

#### Package Manager
```CLI
PM> NuGet\Install-Package Hopex.MultiConfX -Version 23.0.2
```

#### PackageReference
```XML
<PackageReference Include="Hopex.MultiConfX" Version="23.0.2" />
```

#### Paket CLI
```CLI
> paket add Hopex.MultiConfX --version 23.0.2
```

#### Script & Interactive
```CLI
> #r "nuget: Hopex.MultiConfX, 23.0.2"
```

#### Cake
```
// Install Hopex.MultiConfX as a Cake Addin
#addin nuget:?package=Hopex.MultiConfX&version=23.0.2

// Install Hopex.MultiConfX as a Cake Tool
#tool nuget:?package=Hopex.MultiConfX&version=23.0.2
```

# Opportunities

### INI files

| Option | Status |
| --- | ----------- |
| Getting a sheet of sections | :white_check_mark: |
| Getting a sheet of section parameters | :white_check_mark: |
| Getting the value of the section parameter | :white_check_mark: |
| Editing the section name | :white_check_mark: |
| Editing the name of a section parameter | :white_check_mark: |
| Editing the value of a section parameter | :white_check_mark: |
| Deleting the section | :white_check_mark: |
| Deleting the sections | :white_check_mark: |
| Deleting the parameter of a section | :white_check_mark: |
| Deleting the parameters of a section | :white_check_mark: |
| Adding a new value | :white_check_mark: |
| Serialization to JSON format | :white_check_mark: |
| Saving comments | :x: |
| Editing comments | :x: |
| Deleting comments | :x: |

### ENV files

| Option | Status |
| --- | ----------- |
| Getting a sheet of parameters | :white_check_mark: |
| Getting the value of the parameter | :white_check_mark: |
| Editing the name of the parameter | :white_check_mark: |
| Editing the value of the parameter | :white_check_mark: |
| Deleting the parameter | :white_check_mark: |
| Deleting the parameters | :white_check_mark: |
| Adding a new value | :white_check_mark: |
| Serialization to JSON format | :white_check_mark: |
| Saving comments | :x: |
| Editing comments | :x: |
| Deleting comments | :x: |

# How to use

INI and ENV files have a similar data structure, differing only in the presence/absence of sections. Therefore, an example of working only with an INI file will be given below.

```C#
public void Handler()
{
    // Create a INI file handler instance
    Ini IniHandler = new Ini();
    
    // To work with ENV files, use the Env class.
    // Env EnvHandler = new Env();

    /**
    * Let's create three sections and several keys in each of them
    *
    * [Section 1]
    * Key1=Value1
    * Key2="Value of Key2"
    * Key3="Value of Key3"
    * 	
    * [Section 2]
    * Key1="Value of Key1"
    * Key2="Value of Key2"
    * 
    * [Section 3]
    * Key1="Value of Key1"
    */
    IniHandler.Data
        .AddItem(
            section: "Section 1",
            key: "Key1",
            value: "Value1"
        )
        .AddItem(
            section: "Section 1",
            key: "Key2",
            value: "Value of Key2"
        )
        .AddItem(
            section: "Section 1",
            key: "Key3",
            value: "Value of Key3"
        )
        .AddItem(
            section: "Section 2",
            key: "Key1",
            value: "Value of Key1"
        )
        .AddItem(
            section: "Section 2",
            key: "Key2",
            value: "Value of Key2"
        )
        .AddItem(
            section: "Section 3",
            key: "Key1",
            value: "Value of Key1"
        );
    
    // Let's change the value of the first key of the first section
    IniHandler.Data.EditValue(
        section: "Section 1",
        key: "Key1",
        value: "Value2" // it was Value1
    );
    
    // Let's change the name of the first key of the first section
    IniHandler.Data.EditKey(
        section: "Section 1",
        key: "Key1",
        newKey:  "Key0" // it was Key1
    );
    
    // Let's change the name of the first section
    IniHandler.Data.EditSection(
        section: "Section 1",
        newSection:  "Section 0" // it was Section 1
    );
    
    // Let's see what we got
    Console.WriteLine(IniHandler.Data.ToJson(isIndented: true));
    Console.WriteLine("Sections: " + string.Join(", ", IniHandler.Data.GetSections()));
    Console.WriteLine("Keys of Section 2: " + string.Join(", ", IniHandler.Data.GetKeys("Section 2")));

    /**
    * Console output:
    *
    * {
    *   "Section 0": {
    *     "Key0": "Value2",
    *     "Key2": "Value of Key2",
    *     "Key3": "Value of Key3"
    *   },
    *   "Section 2": {
    *     "Key1": "Value of Key1",
    *     "Key2": "Value of Key2"
    *   },
    *   "Section 3": {
    *     "Key1": "Value of Key1"
    *   }
    * }
    * Sections: Section 0, Section 2, Section 3
    * Keys of Section 2: Key1, Key2
    */
    
    // Let's save the data we created
    IniHandler
        .SetPath("file-of-config")
        .Save();
    
    /**
    * Now file "/file-of-config.ini" will contain the following data:
    *
    * [Section 0]
    * Key0=Value2
    * Key2="Value of Key2"
    * Key3="Value of Key3"
    * [Section 2]
    * Key1="Value of Key1"
    * Key2="Value of Key2"
    * [Section 3]
    * Key1="Value of Key1"
    */
    
    // Great, now we will gradually remove everything that we have added
    IniHandler.Data
        // Delete the first key of the first section
        .RemoveKey(
            section: "Section 0",
            key: "Key0"
        )
        // And both keys of the second section
        .RemoveKeys(
            section: "Section 2",
            "Key1", "Key2"
        )
        // Delete the first section
        .RemoveSection(
            section: "Section 0"
        )
        // Also the second and third sections
        .RemoveSections(
            "Section 2", "Section 3"
        );

    /**
    * Let's save the result again
    * If everything is done correctly, then now we have an empty file
    */
    IniHandler.Save();
}
```

## Contributors
- [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json)

## License

MIT License
