# whisky
We need statistics of our whisky production during the mashing process in the pot stills.

Measurements will later on be sent from an Arduino, but in the first iterations, we'll use a WPF application to send sample data.

Keywords: Angular, ASP.NET Core 2.1, MongoDB, SignalR Core.

## Configuration
After the first run, a ```Whisky/server/server_settings.json``` file will be generated, where you can enter your desired username and password for API and a connection string for MongoDB.
Default username is ```username``` and default password is ```password```

## Our use
We'll deploy this on Microsoft Azure and host our database at mlab.com. We will use an Arduino to send measurements. In the first iterations it will however be an WPF application sending the data.