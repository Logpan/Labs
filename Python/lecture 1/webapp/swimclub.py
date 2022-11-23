import statistics

FOLDER = "./swimdata/"
CHARTS = "./charts/"

def get_swim_data(fn):
    name, age, distance, stroke = fn.removesuffix(".txt").split("-")
    with open(FOLDER + fn) as df:
        data = df.readlines()
    times = data[0].strip().split(",")
    converts = []
    for t in times :
        if ":" in t :
            mins, the_rest = t.split(":")
        else :
            mins = 0
            the_rest = t
        sec, hundredths = the_rest.split(".")
        converts.append((int(mins) *60 *100) + (100 * int(sec)) + int(hundredths))
    average = statistics.mean(converts)
    mins = int((average //100) // 60)
    remainder = average - mins * 60 * 100
    secs = int(remainder // 100)
    hundredths = round(remainder - secs * 100)

    return name, age, distance, stroke, average, f"{mins}:{secs}.{hundredths}", times, converts

def convert2range(v, f_min, f_max, t_min, t_max):
    return round(t_min + (t_max - t_min) * ((v - f_min) / (f_max - f_min)), 2)

def produce_bar_charts(fn):
    swimmer, age, distance, stroke, average, average_str, times, converts = get_swim_data(fn)
    title = f"{swimmer} (Under{age}){distance}{stroke}"
    head = f"""
    <!DOCTYPE html>
    <html>
        <head>
            <title> {title} </title>
        </head>
        <body>
            <h3>{title}</h3>
    """

    body = ""

    for c, t in zip(converts,times):
        bar_width = convert2range(c,0,max(converts),0,380)
        svg = f"""
        <svg height="30" width = "400">
            <rect height="30" width="{bar_width}" style ="fill:rgb(0,0,255);" />
        </svg>{t}<br/>
        """
        body = svg + body  

    footer = f"""
            <p> Average time : {average_str} </p>
        </body>
    </html>
    """

    page = head + body + footer
    #work out the new file name 
    save_to = f"{CHARTS}{fn.removesuffix('.txt')}.html"
    # save to the new file name
    with open(save_to, "w") as wf:
        print(page, file=wf)
        
    
    return save_to