SELECT *
FROM DBS311_252NAA12.GLASSES g
LEFT JOIN DBS311_252NAA12.GLASSESINFO gi
ON g.GLASSESINFOID = gi.ID;
SELECT COUNT(*) FROM DBS311_252NAA12.GLASSES;
SELECT owner, table_name
FROM all_tables
WHERE table_name = 'GLASSES';

SELECT COUNT(*) FROM DBS311_252NAA12.GLASSES;
SELECT * FROM DBS311_252NAA12.GLASSES;

SELECT * FROM DBA_TABLES WHERE TABLE_NAME = 'GLASSES';
SELECT owner, synonym_name, table_owner, table_name
FROM all_synonyms
WHERE synonym_name = 'GLASSES';
SELECT * FROM all_users WHERE username = 'DBS311_252NAA12';
------------------------------------------------------------
GRANT SELECT ON DBS311_252NAA12.GLASSES TO dbs311_252naa12;
GRANT SELECT ON DBS311_252NAA12.GLASSESINFO TO dbs311_252naa12;
--------------------------------------------------------------------
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (1, 'Black', 199.99, 'https://example.com/image1.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (2, 'Blue', 189.50, 'https://example.com/image2.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (3, 'Red', 249.00, 'https://example.com/image3.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (4, 'Gray', 210.75, 'https://example.com/image4.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (5, 'Green', 220.00, 'https://example.com/image5.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (6, 'Brown', 195.99, 'https://example.com/image6.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (7, 'White', 205.00, 'https://example.com/image7.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (8, 'Yellow', 215.49, 'https://example.com/image8.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (9, 'Purple', 230.00, 'https://example.com/image9.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (10, 'Orange', 198.99, 'https://example.com/image10.jpg', 1);

INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (11, 'Teal', 225.00, 'https://example.com/image11.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (12, 'Pink', 240.00, 'https://example.com/image12.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (13, 'Cyan', 209.00, 'https://example.com/image13.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (14, 'Magenta', 199.49, 'https://example.com/image14.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (15, 'Maroon', 229.00, 'https://example.com/image15.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (16, 'Navy', 249.99, 'https://example.com/image16.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (17, 'Lime', 189.00, 'https://example.com/image17.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (18, 'Olive', 219.99, 'https://example.com/image18.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (19, 'Silver', 239.00, 'https://example.com/image19.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (20, 'Gold', 259.00, 'https://example.com/image20.jpg', 1);

INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (21, 'Beige', 205.00, 'https://example.com/image21.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (22, 'Turquoise', 199.99, 'https://example.com/image22.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (23, 'Indigo', 210.00, 'https://example.com/image23.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (24, 'Coral', 220.00, 'https://example.com/image24.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (25, 'Lavender', 235.00, 'https://example.com/image25.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (26, 'Mint', 198.00, 'https://example.com/image26.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (27, 'Chocolate', 212.00, 'https://example.com/image27.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (28, 'Charcoal', 225.50, 'https://example.com/image28.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (29, 'Bronze', 239.99, 'https://example.com/image29.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (30, 'Peach', 207.00, 'https://example.com/image30.jpg', 1);

INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (31, 'Steel', 228.00, 'https://example.com/image31.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (32, 'Crimson', 214.00, 'https://example.com/image32.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (33, 'Azure', 220.00, 'https://example.com/image33.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (34, 'Aqua', 229.00, 'https://example.com/image34.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (35, 'Rose', 217.00, 'https://example.com/image35.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (36, 'Sapphire', 248.00, 'https://example.com/image36.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (37, 'Ruby', 235.00, 'https://example.com/image37.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (38, 'Emerald', 226.00, 'https://example.com/image38.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (39, 'Amber', 238.00, 'https://example.com/image39.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (40, 'Onyx', 243.00, 'https://example.com/image40.jpg', 1);

INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (41, 'Ivory', 231.00, 'https://example.com/image41.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (42, 'Sand', 209.00, 'https://example.com/image42.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (43, 'Slate', 216.00, 'https://example.com/image43.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (44, 'Khaki', 219.00, 'https://example.com/image44.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (45, 'Mauve', 227.00, 'https://example.com/image45.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (46, 'Plum', 224.00, 'https://example.com/image46.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (47, 'Rust', 212.00, 'https://example.com/image47.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (48, 'MintGreen', 233.00, 'https://example.com/image48.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (49, 'Mustard', 218.00, 'https://example.com/image49.jpg', 1);
INSERT INTO Glasses (GlassesInfoId, Color, Price, ImageUrl, InStock) VALUES (50, 'Periwinkle', 229.00, 'https://example.com/image50.jpg', 1);
