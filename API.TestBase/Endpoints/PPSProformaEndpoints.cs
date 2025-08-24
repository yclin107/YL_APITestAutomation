using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.TestBase.Endpoints
{
    public static class PPSProformaEndpoints
    {
        //AddCard
        public const string AddCostInit = @"Proforma/{proformaId}/AddCost/Init";
        public const string AddFeeInit = @"Proforma/{proformaId}/AddFee/Init";
        public const string SaveNewCost = @"Proforma/SaveNewCost";
        public const string SaveNewFee = @"Proforma/SaveNewFee";
        public const string CancelCardCreation = @"Proforma/{proformaId}/CancelCardCreation/{cardType}/{cardId}";
        public const string CardCommit = @"Proforma/Card/Commit";

        //Adjustmentg
        public const string Adjustment = @"Proforma/{proformaId}/Adjustment";
        public const string AdjustmentList = @"Proforma/{proformaId}/Adjustment/List";
        public const string AdjustmentOptions = @"Proforma/{proformaId}/Adjustment/Options";
        public const string AdjustmentSave = @"Proforma/{proformaId}/Adjustment/Save";

        // Attachment
        public const string AttachmentNote = @"Attachment/note";
        public const string AttachmentFile = @"Attachment/file";
        public const string Attachment = @"Attachment";

        //Auxiliary
        public const string AuxiliaryTextsDifferences = @"Auxiliary/TextsDifferences";

        //AvailableFunds
        public const string ProformaAvailableFundsTrust = @"Proforma/{proformaId}/AvailableFunds/Trust";
        public const string ProformaAvailableFundsBoa = @"Proforma/{proformaId}/AvailableFunds/Boa";
        public const string ProformaAvailableFundsCredit = @"Proforma/{proformaId}/AvailableFunds/Credit";
        public const string ProformaAvailableFundsSummary = @"Proforma/{proformaId}/AvailableFunds/Summary";
        public const string ProformaAvailableFunds = @"Proforma/{proformaId}/AvailableFunds/{type}/{itemId}";
        public const string ProformaAvailableFundsTrustUpdate = @"Proforma/{proformaId}/AvailableFunds/Trust/{itemId}";

        //Charge
        public const string ProformaCharges = @"Proforma/{proformaId}/Charges";
        public const string ProformaGroupCharges = @"Proforma/{proformaId}/GroupCharges";
        public const string ActionsChargesBulkEdit = @"Proforma/Actions/Charges/BulkEdit";
        public const string ActionsChargesPurge = @"Proforma/Actions/Charges/Purge";
        public const string ActionsChargesCombineInit = @"Proforma/Actions/Charges/Combine/Init";
        public const string ActionsChargesCombineCommit = @"Proforma/Actions/Charges/Combine/Commit";
        public const string ProformaGenericActionsCharges = @"Proforma/GenericActions/Charges/{actionName}";
        public const string ProformaSaveCharge = @"Proforma/SaveCharge";
        public const string HistoryCharges = @"Proforma/{proformaId}/History/Charges";

        //Cost
        public const string ProformaCosts = @"Proforma/{proformaId}/Costs";
        public const string ProformaGroupCosts = @"Proforma/{proformaId}/GroupCosts";
        public const string ActionsCostsBulkEdit = @"Proforma/Actions/Costs/BulkEdit";
        public const string ActionsCostsPurge = @"Proforma/Actions/Costs/Purge";
        public const string ActionsCostsCombineInit = @"Proforma/Actions/Costs/Combine/Init";
        public const string ActionsCostsCombineRecalc = @"Proforma/Actions/Costs/Combine/Recalc";
        public const string ActionsCostsCombineCommit = @"Proforma/Actions/Costs/Combine/Commit";
        public const string ProformaGenericActionsCosts = @"Proforma/GenericActions/Costs/{actionName}";
        public const string ProformaSaveCost = @"Proforma/SaveCost";
        public const string HistoryCosts = @"Proforma/{proformaId}/History/Costs";

        //Diagnostics
        public const string DiagnosticsVersion = @"Diagnostics/version";
        public const string DiagnosticsLogLevel = @"Diagnostics/loglevel";

        // Divide
        // Divide Naming Patterns
        // Divide/Fees/***
        // Divide/Costs/***
        // Divide/Charges/***
        // Divide/Card/***
        // Divide/Cancel
        // Divide/Commit
        public const string DivideFeesInit = @"Divide/Fees/Init";
        public const string DivideFeesField = @"Divide/Fees/Field";
        public const string DivideFeesCardField = @"Divide/Fees/Card/Field";
        public const string DivideFeesCardRecalcRate = @"Divide/Fees/Card/RecalcRate";

        public const string DivideCostsInit = @"Divide/Costs/Init";
        public const string DivideCostsField = @"Divide/Costs/Field";
        public const string DivideCostsCardField = @"Divide/Costs/Card/Field";
        public const string DivideCostsCardRecalcRate = @"Divide/Costs/Card/RecalcRate";

        public const string DivideChargesInit = @"Divide/Charges/Init";
        public const string DivideChargesField = @"Divide/Charges/Field";
        public const string DivideChargesCardField = @"Divide/Charges/Card/Field";
        public const string DivideChargesCardRecalcRate = @"Divide/Card/Charges/RecalcRate";

        public const string DivideFeesCardAdd = @"Divide/Fees/Card/Add";
        public const string DivideCostsCardAdd = @"Divide/Costs/Card/Add";
        public const string DivideChargesCardAdd = @"Divide/Charges/Card/Add";
        public const string DivideCardRemove = @"Divide/{cardType}/Card/Remove";

        public const string DivideCardMatter = @"Divide/Card/Matter";
        public const string DivideCancel = @"Divide/Cancel";
        public const string DivideCommit = @"Divide/Commit";

        // Fee
        public const string ProformaFees = @"Proforma/{proformaId}/Fees";
        public const string ProformaFeesLateCount = @"Proforma/{proformaId}/Fees/late/count";
        public const string ProformaFeesLate = @"Proforma/{proformaId}/Fees/late";
        public const string ProformaFeesLateInclude = @"Proforma/Fees/late/include";
        public const string ProformaGroupFees = @"Proforma/{proformaId}/GroupFees";
        public const string ActionsFeesBulkEdit = @"Proforma/Actions/Fees/BulkEdit";
        public const string ActionsFeesPurge = @"Proforma/Actions/Fees/Purge";
        public const string ActionsFeesCombineInit = @"Proforma/Actions/Fees/Combine/Init";
        public const string ActionsFeesCombineRecalc = @"Proforma/Actions/Fees/Combine/Recalc";
        public const string ActionsFeesCombineCommit = @"Proforma/Actions/Fees/Combine/Commit";
        public const string ProformaGenericActionsFees = @"Proforma/GenericActions/Fees/{actionName}";
        public const string ProformaSaveFee = @"Proforma/SaveFee";
        public const string HistoryFees = @"Proforma/{proformaId}/History/Fees";

        // Health
        public const string Health = @"Health";

        // Proforma
        public const string ProformaStatusFilter = @"Proforma/StatusFilter";
        public const string ProformaBucket = @"Proforma/Bucket/{bucketName}";
        public const string ProformaNavigation = @"Proforma/Navigation/{bucketName}";
        public const string GetProformaFilter = @"Proforma/GetProformaFilter/{bucketName}";
        public const string ProformaById = @"Proforma/{id}";
        public const string GetProformaMatterInfo = @"Proforma/GetProformaMatterInfo/{proformaId}";
        public const string ProformaGroupDetails = @"Proforma/Group/Details/{proformaId}";
        public const string ProformaUnlock = @"Proforma/Unlock";
        public const string SaveTotalsInstructions = @"Proforma/SaveTotalsInstructions";
        public const string ProformaFilterValues = @"Proforma/{proformaId}/FilterValues/{cardType}";
        public const string ProformaActions = @"Proforma/Actions";
        public const string ProformaComments = @"Proforma/{proformaId}/comments";
        public const string ProformaActionsAddComment = @"Proforma/Actions/AddComment";
        public const string ProformaActionsForward = @"Proforma/Actions/Forward";
        public const string FindAndReplaceFind = @"Proforma/Actions/FindAndReplace/Find";
        public const string FindAndReplaceReplace = @"Proforma/Actions/FindAndReplace/Replace";
        public const string ProformaActionsSubmit = @"Proforma/Actions/Submit";
        public const string ProformaActionsSubmitCheck = @"Proforma/Actions/Submit/Check";
        public const string ProformaActionsCollaborators = @"Proforma/Actions/Collaborators";
        public const string ProformaActionsInvoiceNarrative = @"Proforma/Actions/InvoiceNarrative";
        public const string ProformaActionsBillPreview = @"Proforma/Actions/BillPreview";
        public const string ProformaActionsPrint = @"Proforma/Actions/Print";
        public const string ProformaActionsPrintDocs = @"Proforma/Actions/Print/Docs/{printRequestId}";
        public const string ProformaGenericActions = @"Proforma/GenericActions/{actionName}";
        public const string ProformaHistoryEdits = @"Proforma/{proformaId}/History/{cardType}/Edits";
        public const string ProformaCardCodes = @"Proforma/{proformaId}/GetCardCodes/{cardType}/{cardId}";
        public const string ProformaReasonCodes = @"Proforma/ReasonCodes";
        public const string ProformaWIP = @"Proforma/WIP";
        public const string ProformaWIPGroupMatterList = @"Proforma/WIP/GroupMatterList/{billingGroup}";
        public const string ProformaWIPFilterList = @"Proforma/WIP/FilterList";
        public const string ProformaCardsLate = @"Proforma/{proformaId}/cards/late";
        public const string ProformaCardsLateCount = @"Proforma/{proformaId}/cards/late/count";
        public const string ProformaCardsLateInclude = @"Proforma//cards/late/include";

        // ProformaGeneration
        public const string ProformaGenerationInit = @"Proforma/Generation/Init";
        public const string ProformaGenerationCancel = @"Proforma/Generation/Cancel";
        public const string ProformaGenerationField = @"Proforma/Generation/Field";
        public const string ProformaGenerationGenerate = @"Proforma/Generation/Generate";
        public const string ProformaGenerationMatterRemove = @"Proforma/Generation/Matter/Remove";
        public const string ProformaGenerationMatterRestore = @"Proforma/Generation/Matter/Restore";
        public const string ProformaGenerationPreview = @"Proforma/Generation/Preview";

        // Search
        public const string SearchOffices = @"Search/Offices";
        public const string SearchJobPositions = @"Search/JobPositions";
        public const string SearchTimekeepers = @"Search/Timekeepers";
        public const string SearchTimekeepersInfo = @"Search/Timekeepers/Info/{timekeeperIndex}";
        public const string SearchWorkingAsTimekeepers = @"Search/WorkingAsTimekeepers";
        public const string SearchCollaborators = @"Search/Collaborators";
        public const string SearchLookup = @"Search/Lookup/{lookupType}";

        // Session
        public const string Session = @"Session";

        // Setting
        public const string Setting = @"Setting";
        public const string SettingReportFinansial = @"Setting/report/financial";

        // SpellCheck
        public const string ProformaSpellCheckInit = @"Proforma/{proformaId}/SpellCheck";
        public const string ProformaSpellCheckSuggestions = @"Proforma/{proformaId}/SpellCheck/Suggestions";
        public const string ProformaSpellCheckDictionary = @"Proforma/{proformaId}/SpellCheck/Dictionary";

        // System
        public const string SystemNotifications = @"System/Notifications";
        public const string SystemNotificationsDismiss = @"System/Notifications/Dismiss/{notificationId}";
        public const string SystemNotificationsDismissAll = @"System/Notifications/Dismiss/All";
        public const string SystemSettings = @"System/Settings";
        public const string SystemLogManager = @"System/LogManager";

        //Transfer
        public const string TransferInit = @"Transfer/Init";
        public const string TransferMatter = @"Transfer/Matter";
        public const string TransferPTA = @"Transfer/PTA";
        public const string TransferUpdatePTA = @"Transfer/UpdatePTA";
        public const string TransferChangeRecalcRate = @"Transfer/ChangeRecalcRate";
        public const string TransferCommitTransfer = @"Transfer/CommitTransfer";
        public const string TransferFeesUndo = @"Transfer/Fees/Undo";
        public const string TransferCostsUndo = @"Transfer/Costs/Undo";
        public const string TransferChargesUndo = @"Transfer/Charges/Undo";

        //Tracking
        public const string ProformaTrackingList = @"proforma/tracking/list";
        public const string ProformaInformation = @"proforma/infos";
    }
}
