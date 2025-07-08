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
GRANT SELECT ON DBS311_252NAA12.SURVEYANSWER TO dbs311_252naa12;
GRANT SELECT ON DBS311_252NAA12.USERINTERACTIONS TO dbs311_252naa12;
GRANT SELECT ON DBS311_252NAA12.USERS TO dbs311_252naa12;
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
------------------------------------------------------------------------------
INSERT INTO GlassesInfo VALUES (1, 'Model A1', 'BrandX', 'Classic black frame', 'Unisex', 'Round', 'Full Rim', 'Casual', 'Medium', 'M', '48-20-140', '25g', 'Acetate', 'Regular', 'Lightweight', 1);

INSERT INTO GlassesInfo VALUES (2, 'Model A2', 'BrandY', 'Blue transparent style', 'Female', 'Cat Eye', 'Half Rim', 'Fashion', 'Small', 'S', '50-18-135', '22g', 'Metal', 'Snug', 'Trendy', 1);
INSERT INTO GlassesInfo VALUES (3, 'Model A3', 'BrandZ', 'Red bold design', 'Male', 'Square', 'Full Rim', 'Sport', 'Large', 'L', '54-19-145', '28g', 'Titanium', 'Loose', 'Durable', 1);
INSERT INTO GlassesInfo VALUES (4, 'Model A4', 'BrandX', 'Gray elegant style', 'Unisex', 'Rectangle', 'Rimless', 'Classic', 'Medium', 'M', '52-18-140', '24g', 'Stainless Steel', 'Regular', 'Flexible', 1);
INSERT INTO GlassesInfo VALUES (5, 'Model A5', 'BrandY', 'Green minimalist look', 'Female', 'Oval', 'Full Rim', 'Retro', 'Small', 'S', '49-17-135', '23g', 'Plastic', 'Snug', 'Eco-friendly', 1);
INSERT INTO GlassesInfo VALUES (6, 'Model A6', 'BrandZ', 'Brown casual frame', 'Male', 'Wayfarer', 'Full Rim', 'Street', 'Large', 'L', '55-20-145', '27g', 'Acetate', 'Regular', 'Shock-resistant', 1);
INSERT INTO GlassesInfo VALUES (7, 'Model A7', 'BrandX', 'White clean design', 'Unisex', 'Aviator', 'Half Rim', 'Elegant', 'Medium', 'M', '52-18-140', '23g', 'Metal', 'Regular', 'Lightweight', 1);
INSERT INTO GlassesInfo VALUES (8, 'Model A8', 'BrandY', 'Yellow bright style', 'Female', 'Cat Eye', 'Full Rim', 'Trendy', 'Small', 'S', '50-18-135', '21g', 'Plastic', 'Snug', 'UV Protection', 1);
INSERT INTO GlassesInfo VALUES (9, 'Model A9', 'BrandZ', 'Purple bold frame', 'Male', 'Rectangle', 'Rimless', 'Business', 'Large', 'L', '54-19-145', '26g', 'Titanium', 'Loose', 'Scratch-resistant', 1);
INSERT INTO GlassesInfo VALUES (10, 'Model A10', 'BrandX', 'Orange fun look', 'Unisex', 'Round', 'Full Rim', 'Casual', 'Medium', 'M', '48-20-140', '25g', 'Acetate', 'Regular', 'Flexible', 1);

INSERT INTO GlassesInfo VALUES (11, 'Model A11', 'BrandY', 'Teal modern frame', 'Female', 'Oval', 'Half Rim', 'Elegant', 'Small', 'S', '49-17-135', '22g', 'Metal', 'Snug', 'Anti-reflective', 1);
INSERT INTO GlassesInfo VALUES (12, 'Model A12', 'BrandZ', 'Pink playful design', 'Male', 'Square', 'Full Rim', 'Street', 'Large', 'L', '55-20-145', '27g', 'Plastic', 'Loose', 'Durable', 1);
INSERT INTO GlassesInfo VALUES (13, 'Model A13', 'BrandX', 'Cyan minimal frame', 'Unisex', 'Rectangle', 'Rimless', 'Classic', 'Medium', 'M', '52-18-140', '24g', 'Stainless Steel', 'Regular', 'Lightweight', 1);
INSERT INTO GlassesInfo VALUES (14, 'Model A14', 'BrandY', 'Magenta fashion look', 'Female', 'Cat Eye', 'Full Rim', 'Retro', 'Small', 'S', '50-18-135', '23g', 'Acetate', 'Snug', 'UV Protection', 1);
INSERT INTO GlassesInfo VALUES (15, 'Model A15', 'BrandZ', 'Maroon elegant design', 'Male', 'Wayfarer', 'Half Rim', 'Formal', 'Large', 'L', '54-19-145', '26g', 'Metal', 'Regular', 'Flexible', 1);
INSERT INTO GlassesInfo VALUES (16, 'Model A16', 'BrandX', 'Navy classic frame', 'Unisex', 'Round', 'Full Rim', 'Classic', 'Medium', 'M', '48-20-140', '25g', 'Plastic', 'Regular', 'Scratch-resistant', 1);
INSERT INTO GlassesInfo VALUES (17, 'Model A17', 'BrandY', 'Lime vibrant style', 'Female', 'Oval', 'Rimless', 'Trendy', 'Small', 'S', '49-17-135', '22g', 'Titanium', 'Snug', 'Anti-reflective', 1);
INSERT INTO GlassesInfo VALUES (18, 'Model A18', 'BrandZ', 'Olive earth tone', 'Male', 'Rectangle', 'Full Rim', 'Street', 'Large', 'L', '55-20-145', '27g', 'Acetate', 'Loose', 'Lightweight', 1);
INSERT INTO GlassesInfo VALUES (19, 'Model A19', 'BrandX', 'Silver modern look', 'Unisex', 'Square', 'Half Rim', 'Casual', 'Medium', 'M', '52-18-140', '24g', 'Metal', 'Regular', 'Flexible', 1);
INSERT INTO GlassesInfo VALUES (20, 'Model A20', 'BrandY', 'Gold luxurious design', 'Female', 'Cat Eye', 'Rimless', 'Elegant', 'Small', 'S', '50-18-135', '23g', 'Stainless Steel', 'Snug', 'UV Protection', 1);

INSERT INTO GlassesInfo VALUES (21, 'Model A21', 'BrandZ', 'Beige soft style', 'Male', 'Rectangle', 'Full Rim', 'Business', 'Large', 'L', '54-19-145', '26g', 'Plastic', 'Regular', 'Shock-resistant', 1);
INSERT INTO GlassesInfo VALUES (22, 'Model A22', 'BrandX', 'Turquoise modern look', 'Unisex', 'Oval', 'Half Rim', 'Classic', 'Medium', 'M', '49-17-135', '22g', 'Acetate', 'Regular', 'Anti-reflective', 1);
INSERT INTO GlassesInfo VALUES (23, 'Model A23', 'BrandY', 'Indigo trendy frame', 'Female', 'Cat Eye', 'Full Rim', 'Fashion', 'Small', 'S', '50-18-135', '23g', 'Plastic', 'Snug', 'Flexible', 1);
INSERT INTO GlassesInfo VALUES (24, 'Model A24', 'BrandZ', 'Coral bright design', 'Male', 'Square', 'Rimless', 'Sport', 'Large', 'L', '55-20-145', '27g', 'Titanium', 'Loose', 'Lightweight', 1);
INSERT INTO GlassesInfo VALUES (25, 'Model A25', 'BrandX', 'Lavender elegant style', 'Unisex', 'Rectangle', 'Full Rim', 'Classic', 'Medium', 'M', '52-18-140', '24g', 'Metal', 'Regular', 'UV Protection', 1);
INSERT INTO GlassesInfo VALUES (26, 'Model A26', 'BrandY', 'Mint fresh look', 'Female', 'Oval', 'Half Rim', 'Retro', 'Small', 'S', '49-17-135', '22g', 'Acetate', 'Snug', 'Scratch-resistant', 1);
INSERT INTO GlassesInfo VALUES (27, 'Model A27', 'BrandZ', 'Chocolate warm frame', 'Male', 'Wayfarer', 'Full Rim', 'Business', 'Large', 'L', '54-19-145', '26g', 'Plastic', 'Regular', 'Lightweight', 1);
INSERT INTO GlassesInfo VALUES (28, 'Model A28', 'BrandX', 'Charcoal minimal frame', 'Unisex', 'Round', 'Rimless', 'Classic', 'Medium', 'M', '48-20-140', '25g', 'Titanium', 'Regular', 'Flexible', 1);
INSERT INTO GlassesInfo VALUES (29, 'Model A29', 'BrandY', 'Bronze elegant style', 'Female', 'Cat Eye', 'Full Rim', 'Elegant', 'Small', 'S', '50-18-135', '23g', 'Metal', 'Snug', 'Shock-resistant', 1);
INSERT INTO GlassesInfo VALUES (30, 'Model A30', 'BrandZ', 'Peach soft look', 'Male', 'Square', 'Half Rim', 'Casual', 'Large', 'L', '55-20-145', '27g', 'Acetate', 'Loose', 'UV Protection', 1);
INSERT INTO GlassesInfo VALUES (31, 'Model A31', 'BrandX', 'Steel urban design', 'Unisex', 'Rectangle', 'Full Rim', 'Classic', 'Medium', 'M', '52-18-140', '24g', 'Metal', 'Regular', 'Scratch-resistant', 1);
INSERT INTO GlassesInfo VALUES (32, 'Model A32', 'BrandY', 'Crimson bold style', 'Female', 'Cat Eye', 'Half Rim', 'Fashion', 'Small', 'S', '50-18-135', '23g', 'Acetate', 'Snug', 'Flexible', 1);
INSERT INTO GlassesInfo VALUES (33, 'Model A33', 'BrandZ', 'Azure fresh frame', 'Male', 'Square', 'Full Rim', 'Street', 'Large', 'L', '55-20-145', '27g', 'Plastic', 'Loose', 'Lightweight', 1);
INSERT INTO GlassesInfo VALUES (34, 'Model A34', 'BrandX', 'Aqua bright style', 'Unisex', 'Oval', 'Rimless', 'Casual', 'Medium', 'M', '49-17-135', '22g', 'Titanium', 'Regular', 'Anti-reflective', 1);
INSERT INTO GlassesInfo VALUES (35, 'Model A35', 'BrandY', 'Rose elegant design', 'Female', 'Cat Eye', 'Full Rim', 'Retro', 'Small', 'S', '50-18-135', '23g', 'Plastic', 'Snug', 'UV Protection', 1);
INSERT INTO GlassesInfo VALUES (36, 'Model A36', 'BrandZ', 'Sapphire modern frame', 'Male', 'Rectangle', 'Half Rim', 'Formal', 'Large', 'L', '54-19-145', '26g', 'Metal', 'Regular', 'Flexible', 1);
INSERT INTO GlassesInfo VALUES (37, 'Model A37', 'BrandX', 'Ruby luxury style', 'Unisex', 'Round', 'Full Rim', 'Elegant', 'Medium', 'M', '48-20-140', '25g', 'Acetate', 'Regular', 'Scratch-resistant', 1);
INSERT INTO GlassesInfo VALUES (38, 'Model A38', 'BrandY', 'Emerald fresh look', 'Female', 'Oval', 'Rimless', 'Trendy', 'Small', 'S', '49-17-135', '22g', 'Titanium', 'Snug', 'Lightweight', 1);
INSERT INTO GlassesInfo VALUES (39, 'Model A39', 'BrandZ', 'Amber classic frame', 'Male', 'Square', 'Full Rim', 'Business', 'Large', 'L', '55-20-145', '27g', 'Plastic', 'Loose', 'Shock-resistant', 1);
INSERT INTO GlassesInfo VALUES (40, 'Model A40', 'BrandX', 'Onyx minimal design', 'Unisex', 'Rectangle', 'Half Rim', 'Classic', 'Medium', 'M', '52-18-140', '24g', 'Metal', 'Regular', 'Anti-reflective', 1);

INSERT INTO GlassesInfo VALUES (41, 'Model A41', 'BrandY', 'Ivory elegant style', 'Female', 'Cat Eye', 'Full Rim', 'Fashion', 'Small', 'S', '50-18-135', '23g', 'Acetate', 'Snug', 'Flexible', 1);
INSERT INTO GlassesInfo VALUES (42, 'Model A42', 'BrandZ', 'Sand soft look', 'Male', 'Square', 'Rimless', 'Casual', 'Large', 'L', '55-20-145', '27g', 'Titanium', 'Loose', 'Lightweight', 1);
INSERT INTO GlassesInfo VALUES (43, 'Model A43', 'BrandX', 'Slate modern design', 'Unisex', 'Oval', 'Full Rim', 'Classic', 'Medium', 'M', '49-17-135', '22g', 'Metal', 'Regular', 'Scratch-resistant', 1);
INSERT INTO GlassesInfo VALUES (44, 'Model A44', 'BrandY', 'Khaki natural style', 'Female', 'Cat Eye', 'Half Rim', 'Retro', 'Small', 'S', '50-18-135', '23g', 'Plastic', 'Snug', 'UV Protection', 1);
INSERT INTO GlassesInfo VALUES (45, 'Model A45', 'BrandZ', 'Mauve trendy look', 'Male', 'Square', 'Full Rim', 'Street', 'Large', 'L', '55-20-145', '27g', 'Acetate', 'Loose', 'Flexible', 1);
INSERT INTO GlassesInfo VALUES (46, 'Model A46', 'BrandX', 'Plum elegant frame', 'Unisex', 'Rectangle', 'Rimless', 'Business', 'Medium', 'M', '52-18-140', '24g', 'Stainless Steel', 'Regular', 'Lightweight', 1);
INSERT INTO GlassesInfo VALUES (47, 'Model A47', 'BrandY', 'Rust vintage style', 'Female', 'Cat Eye', 'Full Rim', 'Classic', 'Small', 'S', '50-18-135', '23g', 'Metal', 'Snug', 'Scratch-resistant', 1);
INSERT INTO GlassesInfo VALUES (48, 'Model A48', 'BrandZ', 'MintGreen fresh look', 'Male', 'Wayfarer', 'Half Rim', 'Sport', 'Large', 'L', '55-20-145', '27g', 'Plastic', 'Loose', 'UV Protection', 1);
INSERT INTO GlassesInfo VALUES (49, 'Model A49', 'BrandX', 'Mustard bold frame', 'Unisex', 'Oval', 'Full Rim', 'Retro', 'Medium', 'M', '49-17-135', '22g', 'Acetate', 'Regular', 'Flexible', 1);
INSERT INTO GlassesInfo VALUES (50, 'Model A50', 'BrandY', 'Periwinkle soft design', 'Female', 'Cat Eye', 'Rimless', 'Fashion', 'Small', 'S', '50-18-135', '23g', 'Titanium', 'Snug', 'Lightweight', 1);
