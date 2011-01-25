
CREATE function  [dbo].[strSplit] (@string VarChar(8000), @Delimiter Char( 1) )
Returns @res Table (ID int Primary Key, val VarChar(8000) )
As
Begin
     If substring (@string, len ( @string), 1)<>@Delimiter
          Set @string= @String+@Delimiter

     Declare @Start int, 
          @word varchar(8000), 
          @CharIndex int, 
          @i int, 
          @Count Int

     set @i=1
     set @Start=1
     set @CharIndex= charindex(@Delimiter, @string, @start)
     set @Count = 0
     
     while (@charindex <> 0)begin
       set @Word= substring(@string, @start, @charindex - @start)
       set @Start= @charindex +1
       set @CharIndex= charindex(@Delimiter, @string, @start)
       Set @Count = @Count + 1
     
       Insert Into @res  values (@i,@word)
       set @i=@i+1
     end
     return
end

