# TCRM
This small project demonstrates full stack development in c#. It mimics a primitive inventory management system, where users of different roles are granted different rights, for example, the administrator can add/remove a specific right of a non-administrator while sales page is not visible to a user logged in as administrator etc.

This project is inspired by [this video tutorial](https://www.youtube.com/playlist?list=PLLWMQd6PeGY0bEMxObA6dtYXuJOGfxSPx), but differs from the tutorial in the following aspects:

- this project uses a toolkit called [MaterialDesignInXaml](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit) to beautify the UI
- this porject uses EntifyFramework to map data between database and entities instead of using Stored Procedures as in the video tutorial
- other adaptions (see points below)

Login Page
- Will prompt a message to user when username/password is wrong
- After press Log in button, a login animation will shown
![Login Page](https://user-images.githubusercontent.com/32868278/105852678-0e7aa400-5fe5-11eb-9a0e-51a346f7631a.jpg)

Sales Page
![Sales Page](https://user-images.githubusercontent.com/32868278/105852682-0fabd100-5fe5-11eb-898e-5d1274d898b6.jpg)

User Rights Management
- Use graphical display to make user rights management more intuitive
- User model has to been changed accordingly (compare to the video tutorial)
![User Rights](https://user-images.githubusercontent.com/32868278/105852681-0f133a80-5fe5-11eb-8496-6d1a4a8063de.jpg)
