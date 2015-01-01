function InsertEvent(event)
{
    var context = getContext();
    var collection = context.getCollection();
    var response = context.getResponse();

    event = JSON.parse(event);

    var nextId = 1;
    var collectionLink = collection.getSelfLink();

    collection.queryDocuments(collectionLink,
        "SELECT * FROM Events e WHERE e.id = '0'",
        {},
        function (error, documents, responseOptions) {
            
            if (typeof documents != "undefined" && documents != null && documents.length === 1) {
                nextId = documents[0].sequenceNumber+1;
                documents[0].sequenceNumber = nextId;
                collection.replaceDocument(documents[0]._self, documents[0]);
            } else {
               
                collection.createDocument(collectionLink, {
                    id: "0",
                    sequenceNumber: 1
                });
                nextId = 1;
            }
            
            event.id = nextId.toString();
            collection.createDocument(collectionLink, event);
            
            response.setBody(nextId);
        });
}