using Godot;
using System;

public partial class Promise<R>
{
    public enum State { Pending, Resolved, Rejected }
    public State state;
    ResolveCallback[] ResolveChain = Array.Empty<ResolveCallback>();
    RejectCallback[] RejectChain = Array.Empty<RejectCallback>();
    FinallyCallback FinallyFunc = null;
    public delegate Promise<R> ResolveCallback(R data);
    public delegate Promise<R> RejectCallback(Exception err);
    public delegate Promise<R> FinallyCallback();
    public delegate void Initilizator(ResolveCallback Resolve, RejectCallback Reject);
    public Promise(Initilizator initilizator)
    {
        state = State.Pending;
        try
        {
            initilizator(Resolve, Reject);
        }
        catch (Exception e)
        {
            Reject(e);
        };
    }
    Promise<R> Resolve(R data)
    {
        if (state != State.Pending) return this;
        state = State.Resolved;
        if (ResolveChain.Length == 0) return FinallyFunc();
        ResolveCallback Resolve0 = ResolveChain[0];
        ResolveChain = Common.RemoveItemFromArray(ResolveChain, Resolve0);
        Promise<R> result;
        try { result = Resolve0(data); } catch (Exception e) { return Reject(e); };
        if (result == null) return this;
        result.ResolveChain = ResolveChain;
        result.RejectChain = RejectChain;
        return result;
    }
    Promise<R> Reject(Exception err)
    {
        if (state != State.Pending) return this;
        state = State.Rejected;
        if (RejectChain.Length == 0) return FinallyFunc();
        RejectCallback Reject0 = RejectChain[0];
        RejectChain = Common.RemoveItemFromArray(RejectChain, Reject0);
        Promise<R> result;
        try { result = Reject0(err); } catch { return FinallyFunc(); };
        if (result == null) return this;
        result.ResolveChain = ResolveChain;
        result.RejectChain = RejectChain;
        result.Reject(err);
        return result;
    }
    public Promise<R> Then(ResolveCallback callback)
    {
        ResolveChain = Common.AppendItemToArray(ResolveChain, callback);
        return this;
    }
    public Promise<R> Catch(RejectCallback callback)
    {
        RejectChain = Common.AppendItemToArray(RejectChain, callback);
        return this;
    }
    public Promise<R> Finally(Action callback)
    {
        FinallyFunc = () =>
        {
            callback();
            return this;
        };
        return this;
    }
    public override string ToString()
    {
        return $"Promise {{<{state}>}}";
    }
}
