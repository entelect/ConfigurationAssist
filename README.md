# ConfigurationAssist
A project to simplify reading config application/web config files in .Net

# Description

A simple section is what I call a section that doesn't belong to a section group. Below I've shown the most basic section configuration possible. There are variances to this. Please see the advanced section in the wiki to see how to do more complex linking of properties, setting defaults and using more complex section types.

# Example

Below is an example of a Configuration Section in an app.config.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <configSections>
    <section name="TestProperties" type="System.Configuration.SingleTagSectionHandler" />
  </configSections>

  <TestProperties
    TestPropertyOne="one"
    TestPropertyTwo="two"
    TestPropertyThree="three" 
    TestPropertyFour="4" 
    TestPropertyFiveAndHalf="5.5"/>
  
</configuration>
```
As you can see here we have a configuration section called TestProperties. The type is the fully namespaced reference to the configuration class to be used along with the assembly that class belongs too.

To set up this class we merely add the ConfigurationAssist reference to that project, then create the class as shown below:

```C#
using System.Configuration;
using ConfigurationAssist.CustomAttributes;

namespace CodeSandboxConfigAssistTests
{
    public class TestProperties
    {
        public string TestPropertyOne { get; set; }
        public string TestPropertyTwo { get; set; }
        public string TestPropertyThree { get; set; }
        public int TestPropertyFour { get; set; }
        public decimal TestPropertyFiveAndHalf { get; set; }
    }
}
```
That's it, the class is configured, now all we need to do is query it out. There is an Interface for this for if you want to use injection. In the example below though, I will just strongly reference a new instance of it.

To retrieve your configuration settings:
```C#
var configAssistant = new ConfigurationAssist.ConfigurationAssist();
var testProperties = configAssistant.ExtractSettings<TestProperties>();
```

Please read the Wiki for examples:
https://github.com/JeremyBOB81/ConfigurationAssist/wiki
