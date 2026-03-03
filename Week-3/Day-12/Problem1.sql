CREATE DATABASE EventDb;

USE EventDb;


----------------Table-1-----------------------------------------------
CREATE TABLE UserInfo (
    EmailId VARCHAR(100) PRIMARY KEY,
    UserName VARCHAR(50) NOT NULL,
    Role VARCHAR(20) NOT NULL,
    Password VARCHAR(20) NOT NULL,

    CONSTRAINT CHK_UserName_Length CHECK (LEN(UserName) BETWEEN 1 AND 50),

    CONSTRAINT CHK_User_Role CHECK (Role IN ('Admin','Participant')),

    CONSTRAINT CHK_Password_Length CHECK (LEN(Password) BETWEEN 6 AND 20)
);

INSERT INTO UserInfo (EmailId, UserName, Role, Password)
VALUES 
('admin@gmail.com', 'AdminUser', 'Admin', 'admin123'),

('rahul@gmail.com', 'Rahul Sharma', 'Participant', 'rahul123'),

('neha@gmail.com', 'Neha Patil', 'Participant', 'neha456'),

('coordinator@gmail.com', 'EventAdmin', 'Admin', 'event789');

--INSERT INTO UserInfo VALUES ('test@gmail.com','TestUser','Manager','test123');

select * from UserInfo;


--------------------------Table-2------------------------------
CREATE TABLE EventDetails (
    EventId INT PRIMARY KEY,
    EventName VARCHAR(50) NOT NULL,
    EventCategory VARCHAR(50) NOT NULL,
    EventDate DATETIME NOT NULL,
    Description VARCHAR(500) NULL,
    Status VARCHAR(20) NOT NULL,

    CONSTRAINT CHK_Event_Status CHECK (Status IN ('Active','In-Active')),

    CONSTRAINT CHK_EventName_Length CHECK (LEN(EventName) BETWEEN 1 AND 50),

    CONSTRAINT CHK_EventCategory_Length CHECK (LEN(EventCategory) BETWEEN 1 AND 50)
);


INSERT INTO EventDetails
(EventId, EventName, EventCategory, EventDate, Description, Status)
VALUES

(1, 'Tech Fest 2026', 'Technology', '2026-04-10 09:00:00',
 'Annual technical festival with workshops and competitions', 'Active'),

(2, 'Cultural Night', 'Cultural', '2026-05-15 18:00:00',
 'Music and dance performances', 'Active'),

(3, 'Sports Meet', 'Sports', '2026-06-01 07:00:00',
 'Inter-college athletic competition', 'In-Active'),

(4, 'AI Conference', 'Technology', '2026-07-20 10:00:00',
 'Conference on Artificial Intelligence', 'Active');

INSERT INTO EventDetails
(EventId, EventName, EventCategory, EventDate, Description, Status)
VALUES
(5, 'Hackothon', 'Technology', '2026-08-20 10:00',
 'Build And Repeat', 'Active');

 select * from EventDetails;


-------------------------Table-3----------------------------------

CREATE TABLE SpeakersDetails (
    SpeakerId INT PRIMARY KEY IDENTITY(1,1),SpeakerName VARCHAR(50) NOT NULL,

    CONSTRAINT CHK_SpeakerName_Length CHECK (LEN(SpeakerName) BETWEEN 1 AND 50));


INSERT INTO SpeakersDetails (SpeakerName)
VALUES
('Dr. Anil Sharma'),
('Prof. Meena Patil'),
('Mr. Rohan Deshmukh'),
('Ms. Kavita Joshi');

select * from SpeakersDetails;