from flask import Flask, session, render_template, request


import os
import swimclub


app = Flask(__name__)
app.secret_key = "fdgfdgdfhgkjghdf; 'odsjgnadg'oig'oidg 'dioag'dsfkldsjfsdkfjsgjgdfdfgja"


@app.get("/")    # This is the default URL - the homepage of our site.
def index():
    return render_template(
        "index.html",
        title="Welcome to the Swimclub System",
    )


def get_swimmers():
    files = os.listdir(swimclub.FOLDER)
    files.remove(".DS_Store")

    keys = ["age", "distance", "stroke", "average", "average_str", "times", "converts"]

    session["swimmers"] = {}
    for file in files:
        name, *data = swimclub.get_swim_data(file)
        if not name in session["swimmers"]:
            session["swimmers"][name] = []
        session["swimmers"][name].append( {  k: v for k, v in zip(keys, data) } )
        session["swimmers"][name][-1]["file"] = file


@app.get("/swimmers")
def get_swimmers_names():
    if not "swimmers" in session:
        get_swimmers()
    return render_template(
        "select.html",
        title = "Here is the list of swimmers",
        data = sorted(session["swimmers"]),
        url = "/files",
        select_id = "swimmer",
    )


@app.get("/files/<swimmer>")    # Think of this as a URL parameter.
def display_swimmers_files(swimmer):
    if not "swimmers" in session:
        get_swimmers()

    if swimmer not in session["swimmers"]:
        return "That swimmer is not part of the swimclub."
    else:
        the_events = []
        for event in session["swimmers"][swimmer]:
            the_events.append(event["file"])
        return the_events


@app.post("/files")
def process_selected_swimmer():
    return render_template(
        "select.html",
        title = "Here is the list of events",
        data = display_swimmers_files(request.form["swimmer"]),
        url = "/drawchart",
        select_id = "event",
    )


@app.post("/drawchart")
def display_swimmers_graph():
    filename = request.form["event"]
    swimclub.produce_bar_chart(filename, "templates/")
    return render_template(
        filename.removesuffix("txt")+"html",
    )


if __name__ == "__main__":
    app.run(debug=True)
