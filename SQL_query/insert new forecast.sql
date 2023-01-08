SELECT * FROM Forecasts
delete Forecasts
insert into Forecasts 
(id,
Consensus_ticker,
Consensus_recommendation,
Consensus_current_price,
Consensus_currency,
Consensus_consensus,
Consensus_min_target,
Consensus_max_target,
Consensus_price_change,
Consensus_price_change_rel,
Targets_ticker,
Targets_analyst,
Targets_company,
Targets_recommendation,
Targets_recommendation_date,
Targets_current_price,
Targets_currency,
Targets_target_price,
Targets_price_change,
Targets_price_change_rel,
Targets_logo_name,
Targets_logo_base_color,
Targets_show_name) 
values 
(
 NEWID(),
'spr',
50.23,
25.3,
'usd',
50.00,
10.00,
20.00,
25.00,
24.00,
 'spr'
,'dsd'
,'SPR'
,'to buy'
,'01.01.2021'
,50.00
,'usd'
,60.00
,30.00
,23.00
,'rsdgsf.txt'
,'029302'
,'trer'
)

CREATE TABLE [dbo].[Forecasts] (
    [Id]                          UNIQUEIDENTIFIER           NOT NULL,
    [Consensus_ticker]            NVARCHAR (30),
    [Consensus_recommendation]    NVARCHAR (30),
    [Consensus_current_price]     DECIMAL (18) ,
    [Consensus_currency]          NVARCHAR (30),
    [Consensus_consensus]         DECIMAL (10) ,
    [Consensus_min_target]        DECIMAL (18) ,
    [Consensus_max_target]        DECIMAL (18) ,
    [Consensus_price_change]      DECIMAL (18) ,
    [Consensus_price_change_rel]  DECIMAL (18) ,
    [Targets_ticker]              NVARCHAR (30) NOT NULL,
    [Targets_analyst]             NVARCHAR (30) NOT NULL,
    [Targets_company]             NVARCHAR (30) NOT NULL,
    [Targets_recommendation]      NVARCHAR (30) NOT NULL,
    [Targets_recommendation_date] NVARCHAR (35) NOT NULL,
    [Targets_current_price]       DECIMAL (18)  NOT NULL,
    [Targets_currency]            NVARCHAR (30) NOT NULL,
    [Targets_target_price]        DECIMAL (18)  NOT NULL,
    [Targets_price_change]        DECIMAL (18)  NOT NULL,
    [Targets_price_change_rel]    DECIMAL (18)  NOT NULL,
    [Targets_logo_name]           NVARCHAR (30) NOT NULL,
    [Targets_logo_base_color]     NVARCHAR (30) NOT NULL,
    [Targets_show_name]           NVARCHAR (40) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

insert into Forecasts (id,Consensus_ticker,          Consensus_recommendation,       Consensus_current_price,    Consensus_currency,Consensus_consensus,       Consensus_min_target,           Consensus_max_target,       Consensus_price_change,Consensus_price_change_rel, Targets_ticker,                 Targets_analyst,            Targets_company,Targets_recommendation,    Targets_recommendation_date,    Targets_current_price,      Targets_currency,Targets_target_price,      Targets_price_change,           Targets_price_change_rel,   Targets_logo_name,Targets_logo_base_color,   Targets_show_name)values (NEWID(), 'SPR', N'Покупать' , 44.35, 'USD', 59.2, 54, 66, 14.85, 33.48, 'SPR', 'Kenneth Herbert', 'RBC Capital', N'Покупать', '2021-11-04T19:35:00+03:00', 44.35, 'USD', 56, 11.65, 26.27, 'US8485741099.png', '#3F6FAF', 'Spirit AeroSystems Holdings')