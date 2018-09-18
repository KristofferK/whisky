# whisky
We need statistics of our whisky production during the mashing process in the pot stills.

Keywords: Angular, ASP.NET Core 2.1, MongoDB, SignalR Core, WPF (to be replaced by Arduino).

## Configuration
After the first run, a ```Whisky/server/server_settings.json``` file will be generated, where you can enter your desired username and password for API and a connection string for MongoDB.
Default username is ```username``` and default password is ```password```.

When sending a PUT request (to add the measurement), a valid Basic authorization header must be sent that matches the credentials entered in the configuration file.

## Our use
We'll deploy this on Microsoft Azure and host our database at mlab.com. We will use an Arduino to send measurements. In the first iterations it will however be an WPF application sending the data.