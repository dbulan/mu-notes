# Delete VIP bless

UPDATE [MEMB_INFO] SET SCFIsVip = 0, SCFVipTill = getdate() WHERE [memb___id] = '{AccountID}'

# Search in HEX

// Search by serial in HEX
SELECT * FROM table WHERE CONV(SUBSTRING(`hex`, 7, 8), 16, 10) = '%serial')

// Search by type and id in HEX
SELECT * FROM table WHERE CONV(SUBSTRING(`hex`, 19, 1), 16, 10) = 12 AND CONV(SUBSTRING(`hex`, 1, 2), 16, 10) = 201