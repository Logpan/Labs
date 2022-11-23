from flask import Flask
import os, swimclub


app = Flask(__name__)

@app.get("/") #This is the default URL - the homepage of our site.
@app.get("/index.html")
def index():
    return "Hello from my first webapp"

@app.get("/hello") #This is the default URL
def hello():
    1 / 0
    return "This is hello from the hello function, not that index imposter!"

##print(f"****dunder name is set to {__name__}")

def get_swimmers():
    files = os.listdir(swimclub.FOLDER)
    files.remove(".DS_Store")
    swimmers = {}
    keys = ["age", "distance", "stroke", "average", "average_str", "times", "converts"]
    for file in files :
        name, *data = swimclub.get_swim_data(file)
        if not name in swimmers:
            swimmers[name] = []
        swimmers[name].append( { k: v for k, v in zip(keys, data) } )
        swimmers[name][-1]["file"] = file
    return swimmers

@app.get("/swimmers")
def get_swimmers_names():
    return str(sorted(get_swimmers().keys()))



@app.get("/files/<swimmer>") #Think of this as a URL parameter.
def display_swimmers_files(swimmer):
    swimmers = get_swimmers()
    ##return f"Hello {swimmer}."
    
    if swimmer not in swimmers :
        return "That swimmer is not part of swimclub"
    else:
        the_events = []
        for event in swimmers[swimmer]:
            the_events.append(event["file"])
        return str(the_events)



if __name__ == "__main__":
    app.run(debug=True)
