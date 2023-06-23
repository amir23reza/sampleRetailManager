CREATE PROCEDURE [dbo].[spUser]
	@Id nvarchar(128)
AS
begin
	/* the message that indicates the number of rows that are affected by the T-SQL statement is not returned as part of the results. */
	set nocount on;

	select *
	from [dbo].Users
	where Id = @Id;

end