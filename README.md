This is an NTier Architecure prototip. At these prototip, I used these technologies. I currently develop these prototip. 

Freamworks: 
Entity Freamwork, Ninject Freamwork, Identity Freamwork, Elmah Freamwork, MVC, WebApi 2.0

Solution:
MVC; N-Tier Architecture(Core,Data,Service,Web);Entity Freamwork(Code First + Loose
Coupling Connection + Eager Loading )for ORM;Ninject For Dependecy Injection; Identity
Freamwork for Authorazation;Elmah Freamwork for Error Logging; Linq Query; Razor For View
Structure; 

Database: Ef 6.0 Code First, MongoDb(I am currently work on these, in the following version that will be avaible.)

Patterns: MVC, Factory, GenericRepository

Note: This is a prototip project. So I ignored some security rules and some code design rules Also this prototip does not have any template
for WebPanel Section.


General Explanation:
Customer can give order using this system. Restaurant can manage it's Menu and take Statistic
about it's product, feedback about Customer Comment's

This Systems Some Proporties:
For Customer

• Customers have to approve their Order via Cell Phone SMS Code

• Customers give Order and take information about Order's status

• Customers can give vote and make a comment about their orders.

• Customers can see their order activity.

For Managements
• Take Customers' feedback about Menu and Orders

• Take Statistic according to Price, Month and Year

• Manage Orders' Status

