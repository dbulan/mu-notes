# Tips & Tricks

getdate() // current datetime

# Delete VIP bless

UPDATE [MEMB_INFO] SET SCFIsVip = 0, SCFVipTill = getdate() WHERE [memb___id] = '{AccountID}'