using Godot;
using System;

public partial class Promise<R>
{
    public static Promise<R> EmptyInitilizator(R data)
    {
        return new Promise<R>((Resolve, Reject) =>
        {
            try
            {
                Resolve(data);
            }
            catch (Exception e)
            {
                Reject(e);
            }
        });
    }
    public enum State { Pending, Resolved, Rejected }
    public State state;
    ResolveCallback[] ResolveChain = Array.Empty<ResolveCallback>();
    RejectCallback[] RejectChain = Array.Empty<RejectCallback>();
    FinallyCallback FinallyFunc = () => { return null; };
    public delegate Promise<R> ResolveCallback(R data);
    public delegate Promise<R> RejectCallback(Exception err);
    public delegate Promise<R> FinallyCallback();
    public delegate void ResolveCallbackWithoutParamsAndReturns();
    public delegate Promise<R> ResolveCallbackWithoutParams();
    public delegate void ResolveCallbackWithoutReturns(R data);
    public delegate void RejectCallbackWithoutParamsAndReturns();
    public delegate Promise<R> RejectCallbackWithoutParams();
    public delegate void RejectCallbackWithoutReturns(Exception err);
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
        if (result == null)
        {
            if (ResolveChain.Length > 0) return Resolve(data);
            else return this;
        };
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
    public Promise<R> Then(ResolveCallbackWithoutParamsAndReturns callback)
    {
        ResolveChain = Common.AppendItemToArray(ResolveChain, (R data) => { callback(); return null; });
        return null;
    }
    public Promise<R> Then(ResolveCallbackWithoutReturns callback)
    {
        ResolveChain = Common.AppendItemToArray(ResolveChain, (R data) => { callback(data); return null; });
        return null;
    }
    public Promise<R> Then(ResolveCallbackWithoutParams callback)
    {
        ResolveChain = Common.AppendItemToArray(ResolveChain, (R data) => { return callback(); });
        return null;
    }
    public Promise<R> Catch(RejectCallback callback)
    {
        RejectChain = Common.AppendItemToArray(RejectChain, callback);
        return this;
    }
    public Promise<R> Catch(RejectCallbackWithoutParamsAndReturns callback)
    {
        RejectChain = Common.AppendItemToArray(RejectChain, (Exception err) => { callback(); return null; });
        return null;
    }
    public Promise<R> Catch(RejectCallbackWithoutReturns callback)
    {
        RejectChain = Common.AppendItemToArray(RejectChain, (Exception err) => { callback(err); return null; });
        return null;
    }
    public Promise<R> Catch(RejectCallbackWithoutParams callback)
    {
        RejectChain = Common.AppendItemToArray(RejectChain, (Exception err) => { return callback(); });
        return null;
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
