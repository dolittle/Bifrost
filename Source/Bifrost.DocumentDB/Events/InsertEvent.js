function InsertEvent()
{
    var context = getContext();
    var collection = context.getCollection();
    var response = context.getResponse();

    collection.querydocuments(collection.getSelfLink(),
        "SELECT * FROM Events",
        {},
        function (error, documents, responseOptions) {

        });

    response.setBody(0);
}