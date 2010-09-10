package ericutility.misc;

import java.lang.reflect.ParameterizedType;
import java.util.EventListener;

import javax.swing.event.EventListenerList;

public class EventObserver<T extends EventListener>
{
    private final EventListenerList listeners = new EventListenerList();
    
    @SuppressWarnings("unchecked")
    private Class<T> getTClass()
    {
        return (Class<T>) ((ParameterizedType) getClass().getGenericSuperclass()).getActualTypeArguments()[0];
    }
    
    public void subscribe(T listener)
    {
        listeners.add(getTClass(), listener);
    }
    
    public void unsubscribe(T listener)
    {
        listeners.remove(getTClass(), listener);
    }
    
    protected T[] getSubscribers()
    {
        return listeners.getListeners(getTClass());
    }
}
