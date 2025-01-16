# [![ChatRoom](/docs/pics/hero.png)](https://www.chatroom.com/)

ChatRoom is a simple chat Service

You can access the development environment [here](https://dev.chatroom.com/) !

## Summary

1. [Requirement](#requirement)
   - [Docker](#docker)
   - [.NET](#net)
   - [Angular](#angular)
2. [Launch](#launch)
   - [Database](#database)
   - [API](#api)
   - [Client APP](#client-app)
3. [Business Process](#business-process)

## Requirement

First of all, you must install the following dependencies, Docker for the database, .NET for the back office part and NodeJS and Angular for the front end part.

We recommend that you install an editor like [VS Code](https://code.visualstudio.com/) or [Jet Brains Rider](https://www.jetbrains.com/fr-fr/rider/) to be able to better manage the environment if you ever want to launch the application in debugger mode.

### Docker

Go to the [Official Docker website](https://www.docker.com/) and install the version that corresponds to your operating system. This dependency will be used to launch the database in a pre-made environment.

### .NET

Install .NET SDK to be able to launch the API, for this you must go to the [SDK download link](https://dotnet.microsoft.com/en-us/download) and download the version corresponding to your system.

#### User Secrets

Once the .NET tools to be able to launch the API are installed, you must then enter the User Secret of the API to be able to launch it, you can follow the [Microsoft documentation](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-8.0&tabs=windows#how-the-secret-manager-tool-works) for details on how you must proceed according to the operating system you are using.

In Windows, on the path `C:\Users\<YOUR_USERNAME>\AppData\Roaming\Microsoft\UserSecrets`, you must copy and paste the `secrets.json` file into a directory with the secret ID corresponding to `c3281ff1-33b2-453f -ba7d-b1cf330a1eaf`.

![User Secrets Screenshot](/docs/pics/screenshot_user_secrets.png)

> [!NOTE]  
> You will find the file directly on the Team Dev channel of the organization's Teams.

### Angular

For the front end application you must [download NodeJS](https://nodejs.org/en/download/package-manager) which will then install the NPM packet manager where you will then have to type the following commands in a terminal.

```bash
~$ npm i -g @angular/cli
~$ npm i -g pnpm
~$ cd ./chatroom-app
~$ pnpm i
```

> [!NOTE]  
> We recommend that you install NodeJS using Node Version Manager (NVM), the tool will allow you to change version of NodeJS and NPM easily if necessary.

## Launch

Now that all the project dependencies are installed, you can launch it without worries by following the series of commands indicated in the 3 steps below.

First of all you must go to the root of the project by typing the following command:

```bash
~$ cd ~/ChatRoom
```

### Database

To launch the database, use the following docker command:

```bash
~$ docker compose up db
```

Check if dotnet ef tools are installed with the good version:
    
```bash
~$ dotnet ef --version
Entity Framework Core .NET Command-line Tools
9.0.0
```
If not install them with the following command:
```bash
~$ dotnet tool install --global dotnet-ef
```

> [!NOTE]  
> Don't forget to run the docker daemon if this is not the case with `~$ sudo dockerd`.

### API

In a second terminal, type the following commands:

```bash
~$ cd ./ChatRoom.Api
~$ dotnet run
```

## Update to database previous version

In the case the migration you have to rollback to is 20241224111800_OfferBatchUpdateAt:

```bash
~$ cd ./ChatRoom.Repository
~$ dotnet ef database update -s ..\ChatRoom.Startup\ 20241224111800_OfferBatchUpdateAt
```

### Client APP

In a final terminal, type the following commands:

```bash
~$ cd ./chatroom-app
~$ pnpm start
```



## BUSINESS PROCESS

### STATUTS MANAGEMENT

Overview of the different statuts in the buying or selling process. To ensure the historic management, we should be able to identify who has done the action and when it has been done. All actions are done at the products/items level. Please find the 4 steps:

**Pending:** 

1st step when a buyer has made a request. The products will be available with the expected quantity, price, minimum expiration date... The Supplier will be able to make an offer from his/her stock. Finally the buyer will be able to accept, decline or make a counter offer at each of the proposal made by the supplier. 2 statuts are available in Pending:

1- Offer request by "buyer"

2- Offer proposal by "supplier"

**Under review:**

2nd step when a buyer has replied to the offers made by suppliers. 3 options each time will be available from accept, decline and update. The last action before moving to Book (Ready to book), will be possible when the supplier have accepted an offer or counter offer from the buyer. Then the Ready to book will be available for the buyer.

Examples: If a buyer accepts an offer from the supplier, the supplier will confirm the availability by accepting the offer and allow the supplier to book the product.
If a a buyer makes a counter offer to the supplier, the supplier will accept the counter offer and allow the supplier to book the product.

1- Offer accepted by buyer

2- Offer declined by buyer

3- Offer updated by buyer

4- Offer accepted by supplier

5- Offer declined by supplier

6- Offer updated by supplier

You need to have 1 and 4 or 3 and 4 before to move to 7

7- Ready to book - Specificity, the booked can be done at the items level or all items

**Booked:**

3rd step when a buyer has booked product, it is an engagement from the buyer to buy these products. However, first, if the batch number/expiration date are not available, the buyer can accept, decline or make a counter offer after the update done by the supplier. Second, the supplier can always make a counter offer on the quantity only or decline the product if any issues. In this process, the buyer will be able to accept, decline or make a counter offer in the "Booked" tab. All actions following a "Make a counter offer" by the buyer will move back the offer to "Under review". Finally all the products available can be moved to order.

1- Booking made by buyer

2- Waiting for batch - if no batch are there yet

3- Offer updated by supplier

4- Offer declined by supplier

5- Offer accepted by buyer

6- Offer declined by buyer

7- Ready to order

**Ordered:**

Process to be defined in the coming days




