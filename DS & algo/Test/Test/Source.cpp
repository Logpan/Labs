#ifdef _DEBUG 
#pragma comment(lib,"sfml-graphics-d.lib") 
#pragma comment(lib,"sfml-audio-d.lib") 
#pragma comment(lib,"sfml-system-d.lib") 
#pragma comment(lib,"sfml-window-d.lib") 
#pragma comment(lib,"sfml-network-d.lib") 
 
#else 
#pragma comment(lib,"sfml-graphics.lib") 
#pragma comment(lib,"sfml-audio.lib") 
#pragma comment(lib,"sfml-system.lib") 
#pragma comment(lib,"sfml-window.lib") 
#pragma comment(lib,"sfml-network.lib") 
#endif 
#pragma comment(lib,"opengl32.lib") 
#pragma comment(lib,"glu32.lib") 

#include <iostream>
#include <SFML/Graphics.hpp>

class LavaManager
{
public:

    enum class FlowDirection { LEFT, RIGHT, DOWN };
    LavaManager()
    {
        addTriangles();
    }

    void flow(FlowDirection t_direction)
    {
        if (t_direction == FlowDirection::LEFT)
        {
            int vertexCount = m_shape.getVertexCount();
            //m_rightPos.x += 10;
            //MoveLeft(10);
            for (int i = vertexCount-1; i > vertexCount-6000; i -= 3)
            {
                sf::Vertex& v1 = m_shape[i];
                sf::Vertex& v2 = m_shape[i - 1];
                sf::Vertex& v3 = m_shape[i - 2];

                v1.position = sf::Vector2f(v1.position.x - 10, v1.position.y);
                v2.position = sf::Vector2f(v2.position.x - 10, v2.position.y);
                v3.position = sf::Vector2f(v3.position.x - 10, v3.position.y);


            }
            for (int i = vertexCount - 6001; i > vertexCount - 12000; i -= 3)
            {
                sf::Vertex& v1 = m_shape[i];
                sf::Vertex& v2 = m_shape[i - 1];
                sf::Vertex& v3 = m_shape[i - 2];

                v1.position = sf::Vector2f(v1.position.x, v1.position.y+10);
                v2.position = sf::Vector2f(v2.position.x , v2.position.y+10);
                v3.position = sf::Vector2f(v3.position.x , v3.position.y+10);
            }

        }
        else  if (t_direction == FlowDirection::DOWN)
        {
            int vertexCount = m_shape.getVertexCount();
            for (int i = 0; i < 6000; i += 3)
            {
                sf::Vertex& v1 = m_shape[i];
                sf::Vertex& v2 = m_shape[i+1];
                sf::Vertex& v3 = m_shape[i+2];

                v1.position = sf::Vector2f(v1.position.x, v1.position.y);
                v2.position = sf::Vector2f(v2.position.x, v2.position.y);

                v3.position = sf::Vector2f(v3.position.x, v3.position.y);
            }
            //MoveBottom(50);
            
        }
    }

    void MoveBottom (int add)
    {
        double lavaWidth = m_bottomPos.y - m_topPos.y;
        double triangleSpacing = lavaWidth / NUM_TRIANGLES;
        sf::Vector2f nextTriangleCoord = m_bottomPos;
        sf::Color color = sf::Color::Black;
        // Add all triangles up front.
        for (int i = 0; i < 3 * NUM_TRIANGLES; i++)
        {
            //creating points of triangles as vertexes 1, 2 and 3
            sf::Vertex v1(nextTriangleCoord);
            sf::Vertex v2(sf::Vector2f(v1.position.x - 7, v1.position.y - 20));
            sf::Vertex v3(sf::Vector2f(v1.position.x + 7, v1.position.y - 20));
            //setting color
            if (i >= 10000 && i <= 20000)
            {
                color = sf::Color::Red;
            }
            else if (i > 20000)
            {
                color = sf::Color::Yellow;
            }
            v1.color = v2.color = v3.color = color;

            //rotating for random angle
            sf::Transform transform;
            transform.rotate(rand() % 90, (v2.position.x + v3.position.x) / 2, v1.position.y - 10);
            v1.position = transform.transformPoint(v1.position);
            v2.position = transform.transformPoint(v2.position);
            v3.position = transform.transformPoint(v3.position);
            //appending them into vertex array
            m_shape.append(v1);
            m_shape.append(v2);
            m_shape.append(v3);
            nextTriangleCoord = sf::Vector2f(nextTriangleCoord.x , nextTriangleCoord.y - triangleSpacing);
        }
    }


    void addTriangles()
    {
        double lavaWidth = m_rightPos.x - m_leftPos.x;
        double triangleSpacing = lavaWidth / NUM_TRIANGLES;
        sf::Vector2f nextTriangleCoord = m_rightPos;
        sf::Color color = sf::Color::Black;
        // Add all triangles up front.
        for (int i = 0; i < 3 * NUM_TRIANGLES; i++)
        {
            //creating points of triangles as vertexes 1, 2 and 3
            sf::Vertex v1(nextTriangleCoord);
            sf::Vertex v2(sf::Vector2f(v1.position.x - 7, v1.position.y - 20));
            sf::Vertex v3(sf::Vector2f(v1.position.x + 7, v1.position.y - 20));
            //setting color
            if (i >= 10000 && i <= 20000)
            {
                color = sf::Color::Red;
            }
            else if (i > 20000)
            {
                color = sf::Color::Yellow;
            }
            v1.color = v2.color = v3.color = color;

            //rotating for random angle
            sf::Transform transform;
            transform.rotate(rand() % 90, (v2.position.x + v3.position.x) / 2, v1.position.y - 10);
            v1.position = transform.transformPoint(v1.position);
            v2.position = transform.transformPoint(v2.position);
            v3.position = transform.transformPoint(v3.position);
            //appending them into vertex array
            m_shape.append(v1);
            m_shape.append(v2);
            m_shape.append(v3);
            nextTriangleCoord = sf::Vector2f(nextTriangleCoord.x - triangleSpacing, nextTriangleCoord.y);
        }
    }

    //rotate function finds the middle of 3 vertexes and rotates them
    void rotate(double angle)
    {
        for (int i = 0; i < m_shape.getVertexCount(); i += 3)
        {
            sf::Transform transform;
            transform.rotate(angle, (m_shape[i + 1].position.x + m_shape[i + 2].position.x) / 2, (m_shape[i].position.y + m_shape[i + 1].position.y) / 2);
            m_shape[i].position = transform.transformPoint(m_shape[i].position);
            m_shape[i + 1].position = transform.transformPoint(m_shape[i + 1].position);
            m_shape[i + 2].position = transform.transformPoint(m_shape[i + 2].position);
        }

    }

    sf::VertexArray const& getVertexArray() const
    {
        return m_shape;
    }

private:
    sf::Vector2f m_leftPos{ 700.0f, 500.0f };
    sf::Vector2f m_rightPos{ 800.0f, 500.0f };
    sf::Vector2f m_bottomPos{ 500.0f,00.0f };
    sf::Vector2f m_topPos{ 500.0f, 50.0f };
    sf::VertexArray m_shape{ sf::Triangles };
    static int constexpr NUM_TRIANGLES{ 10000 };

};




int main() {
    sf::Clock dtClock, fpsTimer;
    sf::RenderWindow window(sf::VideoMode(800, 600), "Too Slow");
    //creating new array with 30000 triangles
    LavaManager lavaManager;
    lavaManager.flow(LavaManager::FlowDirection::DOWN);
    window.setFramerateLimit(60);
    while (window.isOpen()) {
        //event to close window on close button
        sf::Event event;
        while (window.pollEvent(event))
        {
            if (event.type == sf::Event::Closed)
                window.close();
        }

        window.clear(sf::Color(50, 50, 50));
        //no need for for now, as you can rotate them all in function and draw them together
        lavaManager.rotate(5);
        window.draw(lavaManager.getVertexArray());
        window.display();
        float dt = dtClock.restart().asSeconds();
        if (fpsTimer.getElapsedTime().asSeconds() > 1) {
            lavaManager.flow(LavaManager::FlowDirection::LEFT);
            lavaManager.flow(LavaManager::FlowDirection::DOWN);
            fpsTimer.restart();
            std::cout << ((1.0 / dt > 60) ? 60 : (1.0 / dt)) << std::endl;
        }
    }
}