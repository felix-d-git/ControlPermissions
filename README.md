
# Permission Controls

Adds support for Controls to be accessible based on defined permissions.

**Example:**


**App.xaml.cs**
```cs
public partial class App : Application, IPermissionControlApplication
{
	public UserType GetUserType(string username)
	{
		//Query DB or sth.
		return UserType.SuperUser;
	}
}
```
**MainWindow.xaml**
```xml
<!--Button not accessible to SuperUser-->
<Button Content="Click Me"		
		permissions:ControlPermissions.AuthorizedTypes="Users, Operators"
		permissions:ControlPermissions.NoPermissionBehaviour="Disabled">
</Button>
````

User:

<img src="./img/User.png" />

Operator:

<img src="./img/Operator.png" />

SuperUser:

<img src="./img/SuperUser.png" />

Administrator:

<img src="./img/Administrator.png" />
