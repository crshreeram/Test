using System;

namespace AddressProcessing.CSV
{
    public class ContactsMapper : IContactsMapper
    {
        public void MapContactsDetailsFromContacts
        (
            out string contactName,
            out string contactDetails,
            string contactsData,
            string delimiter
        )
        {
            var contactsFields = contactsData.Split(new string[] { delimiter }, StringSplitOptions.None);

            contactName = contactsFields[0];

            contactDetails = contactsFields.Length > 1 ? contactsFields[1] : null;
        }
    }
}