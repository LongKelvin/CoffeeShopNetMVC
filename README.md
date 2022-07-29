<div id="top"></div>

<!-- PROJECT LOGO -->
<br />
<div align="center">
 

  <h3 align="center">CoffeeShop NET MVC</h3>

  <p align="center">
   A small website for coffeeshop
    
  </p>
  <p align="center">
   [Demo Link] https://coffeeshopdevtest.azurewebsites.net/
  </p>
 
</div>

<!-- ABOUT THE PROJECT -->
## About The Project

CoffeeShop_NetMVC is a personal project aimed at small coffee shops that want to advertise or sell their products on the internet.

This is also the first website project I made during my self-study about Asp.net mvc.

<p align="right">(<a href="#top">back to top</a>)</p>

### Built With

This project use some frameworks/libraries bellow:

* [ASP.NET MVC 5](https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/introduction/getting-started)
* [ASP.NET WEB API 2](https://docs.microsoft.com/en-us/aspnet/web-api/overview/getting-started-with-aspnet-web-api/tutorial-your-first-web-api)
* [AngularJs](https://angularjs.org/)
* [Bootstrap](https://getbootstrap.com)
* [JQuery](https://jquery.com)
* [Javascript](https://www.javascript.com/)
<p align="right">(<a href="#top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

### Prerequisites

To start project, we must have:
*  .Net Framework 4.8 or above
*  Visual Studio 2017/2019/2022
*  MS SqlServer
*  MS SqlServer Managerment (optional)

### Installation

_Below is an example of how we can run this project._

1. Download or clone project repository to your local
   ```sh
   git clone https://github.com/LongKelvin/CoffeeShopNetMVC.git
   ```
2. Change connection string in CoffeeShop.Web/ConnectionString.config to match your connection string
   ```config
   <connectionStrings>
	<add name="CoffeeShopConnection" connectionString="Data Source=YOUR_SQLSERVER_DATA_SOURCE;Integrated Security=True;Initial Catalog=CoffeeShopDatabase_MVC; User ID=YOUR_ID;Password=YOU_PASSWORD;MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient"/>
   </connectionStrings>
   ```
3. Change some settings in AppSettings.conifg (if needed) for example
   ```config
   <!--Login with google-->
	<add key="GoogleCLientId" value="813xxxxx45042-xxxxxxxxxxxxxxxxxxxxxxx.apps.googleusercontent.com" />
	<add key="GoogleCLientSecret" value="XXXXXX-jxxxxxxxxxxxxxx5VM5hT" />
   ```
4. Open Visual Studio and start project (remember to select startup project to CoffeeShop.Web)
<p align="right">(<a href="#top">back to top</a>)</p>


<!-- FEATURES -->
## Feature in this web-app

- [x] Login/Register account for client and admin panel (via email password or Facebook/Google Account)
- [x] Manager Product, Product Category, Post ... (CRUD)
- [x] Manager User roles and permission dynamic
- [x] Shopping cart and Payment via SHIPCOD or BANK ACCOUNT (Inprogress)
- [x] Import data from Excel
- [x] Export data to Excel, Pdf
- [x] Implement Statistics with charts
- [x] Implement SPA for Admin Page (AngularJs & Web Api 2)
- [x] Implement UnitOrWork and Repository pattern


<p align="right">(<a href="#top">back to top</a>)</p>



<!-- CONTRIBUTING -->
## Contributing
If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch 
3. Commit your Changes 
4. Push to the Branch 
5. Open a Pull Request

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

LongKelvin- Email: longkelvin101099@gmail.com

Project Link: [https://github.com/LongKelvin/CoffeeShopNetMVC.git](https://github.com/LongKelvin/CoffeeShopNetMVC.git)

<p align="right">(<a href="#top">back to top</a>)</p>






