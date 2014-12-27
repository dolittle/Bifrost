function GetLastCommittedVersion(eventSourceId) {
    var context = getContext();
    var collection = context.getCollection();
    var response = context.getResponse();

    // Filter by eventSourceId - manual sorting to get the highest version number

    collection.querydocuments(collection.getSelfLink(),
        "SELECT * FROM Events",
        {},
        function (error, documents, responseOptions) {

        });

    response.setBody(0);
}