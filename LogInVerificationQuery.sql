SELECT DISTINCT UD.*
FROM UserDetails AS UD
WHERE UD.UserID  = (SELECT U.UserId
FROM Users AS U
WHERE U.UserName = 'jvil' AND U.Pwd = '123')