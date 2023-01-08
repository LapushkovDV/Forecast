﻿CREATE TABLE [dbo].[Forecasts]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Consensus_ticker] NVARCHAR(10) NOT NULL, 
    [Consensus_recommendation] NVARCHAR(10) NOT NULL, 
    [Consensus_current_price] DECIMAL NOT NULL, 
    [Consensus_currency] NVARCHAR(10) NOT NULL, 
    [Consensus_consensus] DECIMAL(10) NOT NULL, 
    [Consensus_min_target] DECIMAL NOT NULL, 
    [Consensus_max_target] DECIMAL NOT NULL, 
    [Consensus_price_change] DECIMAL NOT NULL, 
    [Consensus_price_change_rel] DECIMAL NOT NULL, 
    [Targets_ticker] NVARCHAR(10) NOT NULL, 
    [Targets_analyst] NVARCHAR(30) NOT NULL, 
    [Targets_company] NVARCHAR(10)NOT  NULL, 
    [Targets_recommendation] NVARCHAR(10) NOT NULL, 
    [Targets_recommendation_date] NVARCHAR(35) NOT NULL, 
    [Targets_current_price] DECIMAL NOT NULL, 
    [Targets_currency] NVARCHAR(10) NOT NULL, 
    [Targets_target_price] DECIMAL NOT NULL, 
    [Targets_price_change] DECIMAL NOT NULL, 
    [Targets_price_change_rel] DECIMAL NOT NULL, 
    [Targets_logo_name] NVARCHAR(20) NOT NULL, 
    [Targets_logo_base_color] NVARCHAR(10) NOT NULL, 
    [Targets_show_name] NVARCHAR(40) NOT NULL
)
