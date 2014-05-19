

CREATE PROCEDURE [dbo].[UP_Customer_Register] 
	@CustomerID NVARCHAR(50),
	@Name NVARCHAR(50),
	@Password VARCHAR(50),
	@Result INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @SysNo INT
	
	DECLARE @StartTran INT
	SET @StartTran = @@TRANCOUNT
	IF( @StartTran = 0)
	BEGIN
		BEGIN TRANSACTION
		SET @StartTran = 1
	END
	
	IF NOT EXISTS(SELECT TOP 1 1 FROM dbo.Customer WITH(NOLOCK) WHERE CustomerID = @CustomerID)
	BEGIN
		INSERT INTO [dbo].[Customer]
           ([CustomerID]
           ,[NAME]
           ,[Password]
           ,[InDate]
           ,[InUser]
           ,[EditDate]
           ,[EditUser])
     VALUES
           (@CustomerID
           ,@Name
           ,@Password
           ,GETDATE()
           ,'WebSite'
           ,GETDATE()
           ,'WebSite')
	           
		    
		SELECT @SysNo = @@IDENTITY
		
		INSERT INTO [Leo].[dbo].[Account]
			   ([SysNO]
			   ,[Amount]
			   ,[LockedAmount]
			   ,[InDate]
			   ,[InUser]
			   ,[EditDate]
			   ,[EditUser])
		 VALUES
			   (@SysNo
			   ,0
			   ,0
			   ,GETDATE()
			   ,'WebSite'
			   ,GETDATE()
			   ,'WebSite')

		SET @Result = 1
	END
	ELSE
	BEGIN
		SET @Result = 0
	END
	
	
	IF @StartTran = 1
	BEGIN
		COMMIT TRANSACTION
	END
END

GO