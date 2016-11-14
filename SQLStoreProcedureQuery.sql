-- Select Customers
Create Procedure SelectCustomers
As
Begin
Select * from Customers;
End

--Insert and Update Employee
Create Procedure InsertUpdateCustomers
(
	@Id integer,  
	@Name nvarchar(50),  
	@Age integer,  
	@State nvarchar(50),  
	@Country nvarchar(50),  
	@Action varchar(10)  
)  
As  
Begin  
	if @Action='Insert'  
		Begin  
			Insert into Customers(Name,Age,[State],Country) values(@Name,@Age,@State,@Country);  
		End  
	if @Action='Update'  
		Begin  
			Update Customers set Name=@Name,Age=@Age,[State]=@State,Country=@Country where CustomerID=@Id;  
		End    
End

-- Delete Employee
Create Procedure DeleteEmployees
(
	@Id integer
)
As
Begin
	Delete Customers Where CustomerId = @Id;
End