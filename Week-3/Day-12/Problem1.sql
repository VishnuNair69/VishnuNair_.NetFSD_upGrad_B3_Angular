CREATE DATABASE EventDb;

USE EventDb;


----------------Table-1-----------------------------------------------
CREATE TABLE UserInfo (
    EmailId VARCHAR(100) PRIMARY KEY,
    UserName VARCHAR(50) NOT NULL,
    Role VARCHAR(20) NOT NULL,
    Password VARCHAR(20) NOT NULL,

    CONSTRAINT CHK_UserName_Length 
        CHECK (LEN(UserName) BETWEEN 1 AND 50),

    CONSTRAINT CHK_User_Role 
        CHECK (Role IN ('Admin','Participant')),

    CONSTRAINT CHK_Password_Length 
        CHECK (LEN(Password) BETWEEN 6 AND 20)
);

--------------------------Table-2------------------------------
CREATE TABLE EventDetails (
    EventId INT PRIMARY KEY ,
    EventName VARCHAR(50) NOT NULL,
    EventCategory VARCHAR(50) NOT NULL,
    EventDate DATETIME NOT NULL,
    Description VARCHAR(500) NULL,
    Status VARCHAR(20) NOT NULL,

    CONSTRAINT CHK_Event_Status 
        CHECK (Status IN ('Active','In-Active')),

    CONSTRAINT CHK_EventName_Length 
        CHECK (LEN(EventName) BETWEEN 1 AND 50),

    CONSTRAINT CHK_EventCategory_Length 
        CHECK (LEN(EventCategory) BETWEEN 1 AND 50)
);

-------------------------Table-3----------------------------------

CREATE TABLE SpeakersDetails (
    SpeakerId INT PRIMARY KEY IDENTITY(1,1),
    SpeakerName VARCHAR(50) NOT NULL,

    CONSTRAINT CHK_SpeakerName_Length 
        CHECK (LEN(SpeakerName) BETWEEN 1 AND 50)
);
