# Contents

- [Contents](#contents)
- [Guide to Card Trigger-Effects](#guide-to-card-trigger-effects)
  - [Effects](#effects)
    - [Draw](#draw)
    - [Discard](#discard)
    - [Power](#power)
    - [Happiness](#happiness)
    - [Progress](#progress)
    - [Double](#double)
  - [Triggers](#triggers)
    - [Instant](#instant)
    - [OnUse](#onuse)
    - [TimeOut](#timeout)
    - [Unhappy](#unhappy)
  - [Troubleshooting](#troubleshooting)
    - [FormatException: Input string was not in a correct format.](#formatexception-input-string-was-not-in-a-correct-format)
- [Welcome to GitHub Pages](#welcome-to-github-pages)
    - [Markdown](#markdown)
    - [Jekyll Themes](#jekyll-themes)
    - [Support or Contact](#support-or-contact)
# Guide to Card Trigger-Effects

For objects with Trigger Effects Str - this is where you can add trigger-effect combos to an object (i.e. card/crisis).
The title is the name of the effect - this goes verbatim into the Effect Name section in the Unity Editor.
The vars are variable in length depending on how many variables the effect has.
Each part will list how many vars to enter, the order in crutial.

## Effects

---

### Draw

---
Draws a card  
Vars 1:  
[0] The number of cards drawn  


### Discard

---

Discard a card  
Vars 1:  
[0] The number of cards discarded  


### Power

---

Adjusts the power var of a specific faction - negatives remove, positives add.  
Vars 2:  
[0] The strength of the effect (10 will add 10)  
[1] The faction index to target or the name of the faction  


### Happiness

---

Adjusts the happiness var of a specific faction - negatives remove, positives add.  
Vars 2:  
[0] The strength of the effect (10 will add 10)  
[1] The faction index to target or the name of the faction  


### Progress

---

Adjusts the progress var of a specific faction - negatives remove, positives add. This can be trigger dependant.  
Vars 2:  
[0] The strength of the effect (10 will add 10)  
[1] The faction index to target or the name of the faction  


### Double

---

Doubles the object's effects to be 2x as powerful.  
Vars:  
[0]+ Putting each entry as a effect name will ignore this effect in the doubling process. (i.e. put the names of the effects you want to ignore in the list)  

---

## Triggers

---


### Instant

---

The Default one. If trigger Name is blank this will be used by default. This causes the effect to trigger on use (i.e. on card use or on Crisis Start)  
No vars needed.


### OnUse

---

Triggers when a card is used.  
Vars 1:  
[0] The card name that triggers this effect.  


### TimeOut

---

Triggers after a specific number of turns has passed.  
Vars 1:  
[0] The number of turns to wait before triggering.  


### Unhappy

---

Triggers when a faction is unhappy.  
Vars 1:  
[0] The faction index to trigger on.  

## Troubleshooting

### FormatException: Input string was not in a correct format.
You have your input vars in the wrong format. Make sure they're in the right order

# Welcome to GitHub Pages

You can use the [editor on GitHub](https://github.com/Thalpy/Politicards/edit/main/README.md) to maintain and preview the content for your website in Markdown files.

Whenever you commit to this repository, GitHub Pages will run [Jekyll](https://jekyllrb.com/) to rebuild the pages in your site, from the content in your Markdown files.

### Markdown

Markdown is a lightweight and easy-to-use syntax for styling your writing. It includes conventions for

```markdown
Syntax highlighted code block

# Header 1
## Header 2
### Header 3

- Bulleted
- List

1. Numbered
2. List

**Bold** and _Italic_ and `Code` text

[Link](url) and ![Image](src)
```

For more details see [Basic writing and formatting syntax](https://docs.github.com/en/github/writing-on-github/getting-started-with-writing-and-formatting-on-github/basic-writing-and-formatting-syntax).

### Jekyll Themes

Your Pages site will use the layout and styles from the Jekyll theme you have selected in your [repository settings](https://github.com/Thalpy/Politicards/settings/pages). The name of this theme is saved in the Jekyll `_config.yml` configuration file.

### Support or Contact

Having trouble with Pages? Check out our [documentation](https://docs.github.com/categories/github-pages-basics/) or [contact support](https://support.github.com/contact) and we’ll help you sort it out.
