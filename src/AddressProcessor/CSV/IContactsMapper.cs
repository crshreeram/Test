namespace AddressProcessing.CSV
{
    public interface IContactsMapper
    {
        void MapContactsDetailsFromContacts(out string contactName, out string contactDetails, string contactsData,string delimiter);
    }
}