# ConcretioTest
Concretio Test

You can find my complete code at github:  https://github.com/Amit1150/ConcretioTest

This is sample application, which allow user to login using github account, and it will list down all their gist and also allow them to add a new gist.

In web.config file, We kept the ClientID, ClientSecret and AppName which is being used for Github login.

It has script file (file location :  Scripts/concretio.script.js), It contain all the js event/functions like click e.t.c.

All the server side codes are written in (file location : Controllers/HomeController.cs) HomeController file. 

It has mainly to 2 pages.
 1. Index (file location :  Views/Home/Index.cshtml): It is being used as login page, where user will find a login button, on which user need to click to login using Github.
 
 2. MyProfile (file location :  Views/Home/MyProfile.cshtml): It will list down all the gist, which was created by logged in user. and also there is textbox to create a new gist.
 
 
I have hosted this application at  http://stackgeek-001-site1.gtempurl.com
 




